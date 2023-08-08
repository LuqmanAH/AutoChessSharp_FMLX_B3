namespace AutoChessSharp.Core;

//! Overall experimental algorithm, may return unexpected bugs
public partial class GameRunner
{
    /// <summary>
    /// Simulate a randomized war process in autochess, current version supports fixed small range random number generator
    /// </summary>
    /// <returns>SortedDictionary where the key represents IPiece left after the clash and the value represents the corresponding player</returns>
    public SortedDictionary<int, IPlayer> GameClash()
    {
        SortedDictionary<int, IPlayer> afterClash = new SortedDictionary<int, IPlayer>();
        Random rng = new Random();
        
        List<IPiece>[] eachPlayerPieces = GetEachPlayerPiece();
        
        List<IPiece>[] shuffledPlayerPieces = PlayerPieceShuffle(eachPlayerPieces, rng);
        
        List<IPiece>[] playerSurvivorPieces = SurvivorRandomExtract(shuffledPlayerPieces, rng, 3);
        
        int[] playerSurvivorsCount = SurvivorsCount(playerSurvivorPieces);

        int survivorIndex = 0;
        foreach (var player in _playerDetail.Keys)
        {
            afterClash.Add(playerSurvivorsCount[survivorIndex], player);
            survivorIndex ++;
        }

        afterClashEvent.Invoke(afterClash);

        return afterClash;
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
        int firstExtract = new();

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

    private bool ResetPlayerPieces(PlayerInfo playerInfo, List<IPiece> playerSurvivorPiece)
    {
        if (playerSurvivorPiece == null)
        {
            return false;
        }

        playerInfo.SetPieces(playerSurvivorPiece);

        return true;
    }

    private int[] SurvivorsCount(List<IPiece>[] survivorsToCount)
    {
        int[] playerSurvivorsCount = new int[PlayersLeft()];

        playerSurvivorsCount[0] = survivorsToCount[0].Count;
        playerSurvivorsCount[1] = survivorsToCount[1].Count;

        return playerSurvivorsCount;
    }

    /// <summary>
    /// Extract the loser that will lose HP after a clash
    /// </summary>
    /// <param name="clashResult"></param>
    /// <returns> key value pair representing the damage received as the key, and the damaged player as the value. Damage received based on winner remaining pieces </returns>
    /// <exception cref="NullReferenceException"></exception>
    //TODO can implement Delegate clashLoser and clashWinner to GameClash
    
    private void ClashLoserEvent(SortedDictionary<int, IPlayer> clashResult)
    {
        KeyValuePair<int, IPlayer> loserPair = clashResult.First();
        KeyValuePair<int, IPlayer> winnerPair = clashResult.Reverse().First();
        KeyValuePair<int, IPlayer> playerDamaged = new KeyValuePair<int, IPlayer>(winnerPair.Key, loserPair.Value);

        _clashLoser = playerDamaged;
    }

    private void ClashWinnerEvent(SortedDictionary<int, IPlayer> clashResult)
    {
        _clashWinner = clashResult.Reverse().First();
    }

    public KeyValuePair<int, IPlayer> GetClashLoser()
    {
        return _clashLoser;
    }

    public KeyValuePair<int, IPlayer> GetClashWinner()
    {
        return _clashWinner;
    }

    /// <summary>
    /// Damage the player HP based on the clash winner remaining pieces, when the player takes fatal damage (below zero) set the HP to 0 instead
    /// </summary>
    /// <param name="playerDamaged"></param>
    /// <returns>The updated player HP after the damage has done</returns>
    public int DecreasePlayerHealth(KeyValuePair<int, IPlayer> playerDamaged)
    {
        var damagedPlayerInfo = _playerDetail.Where(kvp => kvp.Key == playerDamaged.Value).Select(kvp => kvp.Value).FirstOrDefault();
        int updatedPlayerHealth = damagedPlayerInfo.GetHealth() - playerDamaged.Key;

        if (updatedPlayerHealth < 0)
        {
            damagedPlayerInfo.SetHealth(0);
            return 0;
        }

        damagedPlayerInfo.SetHealth(updatedPlayerHealth);
        return updatedPlayerHealth;
    }
}
