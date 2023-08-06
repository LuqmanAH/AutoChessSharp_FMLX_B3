namespace AutoChessSharp.Core;

//! 
public partial class GameRunner
{
    /// <summary>
    /// Simulate a randomized war process in autochess, current version supports fixed small range random number generator
    /// </summary>
    /// <returns>SortedDictionary where the key represents piece left after the clash and the value represents the corresponding player</returns>
    public SortedDictionary<int, IPlayer> GameClash()
    {
        SortedDictionary<int, IPlayer> afterClash = new SortedDictionary<int, IPlayer>();
        Random rng = new Random();
        
        List<Piece>[] eachPlayerPieces = GetEachPlayerPiece();
        
        List<Piece>[] shuffledPlayerPieces = PlayerPieceShuffle(eachPlayerPieces, rng);
        
        List<Piece>[] playerSurvivorPieces = SurvivorRandomExtract(shuffledPlayerPieces, rng, 3);
        
        int[] playerSurvivorsCount = SurvivorsCount(playerSurvivorPieces);

        int survivorIndex = 0;
        foreach (var player in _playerDetail.Keys)
        {
            afterClash.Add(playerSurvivorsCount[survivorIndex], player);
            survivorIndex ++;
        }

        return afterClash;
    }

    /// <summary>
    /// Extract every available pieces in the hand of in game player
    /// </summary>
    /// <returns>Array of player pieces list, not sorted</returns>
    private List<Piece>[] GetEachPlayerPiece()
    {
        List<Piece>[] eachPlayerPieces = new List<Piece>[PlayersLeft()];
        foreach (var playerData in _playerDetail.Values)
        {
            for (int playerID = 0; playerID < PlayersLeft(); playerID++)
            {
                if (eachPlayerPieces[playerID] is null)
                {
                    eachPlayerPieces[playerID] = playerData.GetPieces();
                }
            }
        }
        return eachPlayerPieces;
    }

    /// <summary>
    /// Shuffle the list of pieces by a random number generator. The rng does not have boundary values
    /// </summary>
    /// <param name="piecesToShuffle"></param>
    /// <param name="rng"></param>
    /// <returns>Array of player pieces list, shuffled randomly</returns>
    private List<Piece>[] PlayerPieceShuffle(List<Piece>[] piecesToShuffle, Random rng)
    {
        List<Piece>[] shuffledPlayerPieces = new List<Piece>[PlayersLeft()];
        for (int playerID = 0; playerID < PlayersLeft(); playerID++)
        {
            shuffledPlayerPieces[playerID] = piecesToShuffle[playerID].OrderByDescending(playerID => rng.Next()).ToList();
        }
        return shuffledPlayerPieces;
    }

    /// <summary>
    /// Simulate piece death by subtracting pieces by a randomly generated amount
    /// </summary>
    /// <param name="survivorsToExtract"></param>
    /// <param name="rng"></param>
    /// <param name="maxAmount"></param>
    /// <returns>Array of player pieces list left after the subtraction</returns>
    private List<Piece>[] SurvivorRandomExtract(List<Piece>[] survivorsToExtract, Random rng, int maxAmount)
    {
        List<Piece>[] playerSurvivorPieces = new List<Piece>[PlayersLeft()];

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

    //TODO this still doesnt work [Very Experimental]
    private bool ResetPlayerPieces(PlayerInfo playerInfo, List<Piece> playerSurvivorPiece)
    {
        if (playerSurvivorPiece == null)
        {
            return false;
        }

        playerInfo.SetPieces(playerSurvivorPiece);

        return true;
    }

    private int[] SurvivorsCount(List<Piece>[] survivorsToCount)
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
    public KeyValuePair<int, IPlayer> GetClashLoser(SortedDictionary<int, IPlayer> clashResult)
    {
        if (clashResult == null)
        {
            throw new NullReferenceException(message: "Clash not yet started!");
        }
        KeyValuePair<int, IPlayer> loserPair = clashResult.First();
        KeyValuePair<int, IPlayer> winnerPair = clashResult.Reverse().First();
        KeyValuePair<int, IPlayer> playerDamaged = new KeyValuePair<int, IPlayer>(winnerPair.Key, loserPair.Value);

        return playerDamaged;
    }

    public KeyValuePair<int,IPlayer> GetClashWinner(SortedDictionary<int, IPlayer> clashResult)
    {
        if (clashResult == null)
        {
            throw new NullReferenceException(message: "Clash not yet started!");
        }

        return clashResult.Reverse().First();
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
