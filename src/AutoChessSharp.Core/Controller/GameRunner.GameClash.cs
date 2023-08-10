namespace AutoChessSharp.Core;

//* Bugs handled, but algorithm does not consider each piece atk and hp in the decision making
public partial class GameRunner
{
    /// <summary>
    /// Simulate a randomized war process in autochess, current version supports fixed small range random number generator
    /// </summary>
    /// <returns>Dictionary that has been sorted descending by the piece amount value left after the clash</returns>
    public Dictionary<IPlayer, int> GameClash()
    {
        Dictionary<IPlayer, int> clashResult = new Dictionary<IPlayer, int>();
        Random rng = new Random();
        
        List<IPiece>[] eachPlayerPieces = GetEachPlayerPiece();
        
        List<IPiece>[] shuffledPlayerPieces = PlayerPieceShuffle(eachPlayerPieces, rng);
        
        List<IPiece>[] playerSurvivorPieces = SurvivorRandomExtract(shuffledPlayerPieces, rng, 3);
        
        int[] playerSurvivorsCount = SurvivorsCount(playerSurvivorPieces);

        int survivorIndex = 0;
        foreach (var player in _playerDetail.Keys)
        {
            clashResult.Add(player, playerSurvivorsCount[survivorIndex]);
            survivorIndex ++;
        }

        var sortedResult = clashResult.OrderByDescending(kvp => kvp.Value)
                        .ToDictionary(kvp => kvp.Key, kvp =>kvp.Value);

        afterClashEvent.Invoke(sortedResult);

        return sortedResult;
    }

    /// <summary>
    /// Extract every available pieces in the hand of in game player
    /// </summary>
    /// <returns>Array of player pieces list, not sorted</returns>
    private List<IPiece>[] GetEachPlayerPiece()
    {
        List<IPiece>[] eachPlayerPieces = new List<IPiece>[PlayersLeft()];
        int playerID = 0;
        foreach (var playerData in _playerDetail.Values)
        {
            eachPlayerPieces[playerID] = playerData.GetPieces();
            playerID ++;

        }
        return eachPlayerPieces;
    }

    /// <summary>
    /// Shuffle the list of pieces by a random number generator. The rng does not have boundary values
    /// </summary>
    /// <param name="piecesToShuffle"></param>
    /// <param name="rng"></param>
    /// <returns>Array of player pieces list, shuffled randomly</returns>
    private List<IPiece>[] PlayerPieceShuffle(List<IPiece>[] piecesToShuffle, Random rng)
    {
        List<IPiece>[] shuffledPlayerPieces = new List<IPiece>[PlayersLeft()];
        for (int playerID = 0; playerID < PlayersLeft(); playerID++)
        {
            shuffledPlayerPieces[playerID] = piecesToShuffle[playerID].OrderByDescending(playerID => rng.Next()).ToList();
        }
        return shuffledPlayerPieces;
    }

    /// <summary>
    /// Simulate IPiece death by subtracting pieces by a randomly generated amount
    /// </summary>
    /// <param name="survivorsToExtract"></param>
    /// <param name="rng"></param>
    /// <param name="maxAmount"></param>
    /// <returns>Array of player pieces list left after the subtraction</returns>
    private List<IPiece>[] SurvivorRandomExtract(List<IPiece>[] survivorsToExtract, Random rng, int maxAmount)
    {
        List<IPiece>[] playerSurvivorPieces = new List<IPiece>[PlayersLeft()];

        //* might be buggy
        int firstExtract = maxAmount + 1;

        for (int playerID = 0; playerID < PlayersLeft(); playerID++)
        {
            int secondExtract = rng.Next(0, maxAmount);

            while(firstExtract == secondExtract)
            {
                secondExtract = rng.Next(0, maxAmount);
            }

            firstExtract = secondExtract;
            
            playerSurvivorPieces[playerID] = survivorsToExtract[playerID].Take(firstExtract).ToList();
        }
        foreach (var playerDetail in _playerDetail)
        {
            ResetPlayerPieces(playerDetail.Value, playerSurvivorPieces[playerDetail.Key.GetID() - 1]);
        }

        return playerSurvivorPieces;
    }

    /// <summary>
    /// Attempt to reset the pieces owned by the player after the clash
    /// </summary>
    /// <param name="playerInfo"></param>
    /// <param name="playerSurvivorPiece"></param>
    /// <returns>true when the clash is not tied</returns>
    private bool ResetPlayerPieces(PlayerInfo playerInfo, List<IPiece> playerSurvivorPiece)
    {
        if (playerSurvivorPiece == null)
        {
            return false;
        }

        playerInfo.SetPieces(playerSurvivorPiece);

        return true;
    }

    /// <summary>
    /// Extracts the number of pieces left after clash event
    /// </summary>
    /// <param name="survivorsToCount"></param>
    /// <returns>array of integer representing pieces left from each player</returns>
    private int[] SurvivorsCount(List<IPiece>[] survivorsToCount)
    {
        int[] playerSurvivorsCount = new int[PlayersLeft()];

        //! iterate
        for (int i = 0; i < PlayersLeft(); i++)
        {
            playerSurvivorsCount[i] = survivorsToCount[i].Count;
        }

        return playerSurvivorsCount;
    }

    /// <summary>
    /// Set the clash loser based on the clash event. Sets key value pair representing the damage received as the value, and the damaged player as the key. Damage received based on winner remaining pieces.
    /// </summary>
    /// <param name="clashResult"></param>
    //TODO can implement Delegate clashLoser and clashWinner to GameClash
    private void ClashLoserEvent(Dictionary<IPlayer, int> clashResult)
    {
        KeyValuePair<IPlayer, int> loserPair = clashResult.Reverse().First();
        KeyValuePair<IPlayer, int> winnerPair = clashResult.First();
        if (loserPair.Value == winnerPair.Value)
        {
            KeyValuePair<IPlayer, int> playerDamaged = new KeyValuePair<IPlayer, int>(loserPair.Key, -1);
            _clashLoser = playerDamaged;
        }
        else
        {
            
            KeyValuePair<IPlayer, int> playerDamaged = new KeyValuePair<IPlayer, int>(loserPair.Key, winnerPair.Value);
            _clashLoser = playerDamaged;
        }

    }

    /// <summary>
    /// Set the clash winner based on the clash event. Sets key value pair representing the number of pieces left as the value, and the winning player as the key
    /// </summary>
    /// <param name="clashResult"></param>
    private void ClashWinnerEvent(Dictionary<IPlayer, int> clashResult)
    {
        if (clashResult.All(kvp => kvp.Value == clashResult.First().Value))
        {
            _clashWinner = new KeyValuePair<IPlayer, int>(clashResult.First().Key, -1);
        }
        _clashWinner = clashResult.First();
    }

    /// <summary>
    /// Extract the clash loser key value pair after the clash event
    /// </summary>
    /// <returns>Key value pair representing damaged player as the key, and the damage done as the value</returns>
    public KeyValuePair<IPlayer, int> GetClashLoser()
    {
        return _clashLoser;
    }

    /// <summary>
    /// Extract the clash winner key value pair after the clash event
    /// </summary>
    /// <returns>Key value pair representing winnning player as the key, and the remaining pieces count as the value</returns>
    public KeyValuePair<IPlayer, int> TryGetClashWinner()
    {
        return _clashWinner;
    }

    /// <summary>
    /// Damage the player HP based on the clash winner remaining pieces, when the player takes fatal damage (below zero) set the HP to 0 instead
    /// </summary>
    /// <param name="playerDamaged"></param>
    /// <returns>The updated player HP after the damage has done</returns>
    public bool TryDecreasePlayerHealth(KeyValuePair<IPlayer, int> playerDamaged)
    {
        if (playerDamaged.Value < 0)
        {
            return false;
        }
        var damagedPlayerInfo = _playerDetail.Where(kvp => kvp.Key == playerDamaged.Key)
                                            .Select(kvp => kvp.Value)
                                            .FirstOrDefault();

        int updatedPlayerHealth = damagedPlayerInfo.GetHealth() - playerDamaged.Value;

        if (updatedPlayerHealth < 0)
        {
            damagedPlayerInfo.SetHealth(0);
            return false;
        }

        damagedPlayerInfo.SetHealth(updatedPlayerHealth);
        return true;
    }
}
