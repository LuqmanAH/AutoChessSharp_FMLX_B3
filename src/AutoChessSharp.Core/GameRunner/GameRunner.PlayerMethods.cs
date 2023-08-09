namespace AutoChessSharp.Core;

public partial class GameRunner
{
    /// <summary>
    /// attempts to add player, can not add when the same instance of IPlayer twice
    /// </summary>
    /// <param name="player"></param>
    /// <returns>true when player addition success</returns>
    public bool AddPlayer(IPlayer player)
    {
        bool addSuccess = _playerDetail.TryAdd(player, new PlayerInfo());
        return addSuccess;
    }
    
    /// <summary>
    /// extracts signed up players that currently in game regardless of the HP value
    /// </summary>
    /// <returns>dictionary with IPlayer instances as the key, and player info as the value</returns>
    public Dictionary<IPlayer, PlayerInfo> GetInGamePlayers()
    {
        return _playerDetail;
    }

    /// <summary>
    /// extract signed up player with the given index
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <returns>IPlayer instance that has the corresponding index in the dictionary</returns>
    public IPlayer GetPlayer(int playerIndex)
    {
        return (Player)_playerDetail.Keys.ElementAt(playerIndex - 1);
    }

    //// buat separasi methods, sesuai dengan stat yang diinput user
    /// <summary>
    /// extract the player info instance associated to the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Dictionary containing numeric player stats. namely, level, exp, hp, and gold as the key and its corresponding values as the value</returns>
    public Dictionary<string, int> GetPlayerStats(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        Dictionary<string, int> playerStats = new();

        KeyValuePair<string, int>[] stats = new KeyValuePair<string, int>[4]{
            new KeyValuePair<string, int>("Current Level", playerInfo.GetLevel()),
            new KeyValuePair<string, int>("Experience", playerInfo.GetExperience()),
            new KeyValuePair<string, int>("Health Point", playerInfo.GetHealth()),
            new KeyValuePair<string, int>("Gold", playerInfo.GetGold()),
        };

        foreach (KeyValuePair<string, int> stat in stats)
        {
            playerStats.Add(stat.Key, stat.Value);
        }
        return playerStats;
    }

    /// <summary>
    /// extract the level info of the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>integer representing the player level</returns>
    public int GetPlayerCurrentLevel(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetLevel();

    }
    
    /// <summary>
    /// extract the exp info of the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>integer representing the player experience</returns>
    public int GetPlayerCurrentExperience(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetExperience();

    }
    
    /// <summary>
    /// extract the health info of the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>integer representing the player health point</returns>
    public int GetPlayerCurrentHP(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetHealth();

    }
    
    /// <summary>
    /// extract the gold info of the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>integer representing the player gold</returns>
    public int GetPlayerCurrentGold(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetGold();

    }

    /// <summary>
    /// extract the list of pieces owned by the given IPlayer instance
    /// </summary>
    /// <param name="player"></param>
    /// <returns>list containing pieces</returns>
    public List<IPiece> GetPlayerPiece(IPlayer player)
    {
        List<IPiece> playerPieces = new();
        foreach (var playerData in _playerDetail)
        {
            if (playerData.Key == player)
            {
                playerPieces = playerData.Value.GetPieces();
            }
        }
        return playerPieces;
    }

    /// <summary>
    /// extracts a dictionary of the current health point of in game players
    /// </summary>
    /// <returns>dictionary containing IPlayer as the key, and integer as the value, exception when no player has signed up</returns>
    /// <exception cref="NullReferenceException"></exception>
    public Dictionary<IPlayer, int> ShowPlayerHealth()
    {
        if (_playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Players Yet!");
        }

        Dictionary<IPlayer, int> PlayersHealth = new();
        foreach (var details in _playerDetail)
        {
            IPlayer playerInGame = details.Key;
            int playerHealthPoint = details.Value.GetHealth();

            PlayersHealth.Add(playerInGame, playerHealthPoint);
        }
        return PlayersHealth;
    }

    /// <summary>
    /// extracts the health point of the given IPlayer that has signed up
    /// </summary>
    /// <param name="player"></param>
    /// <returns>integer representing the health of the given IPlayer, exception when player given is not signed up or when no player has signed up</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public int ShowPlayerHealth(IPlayer player)
    {
        if (_playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Players Yet!");
        }

        if (!_playerDetail.ContainsKey(player))
        {
            throw new KeyNotFoundException(message : "Player Not Found");
        }

        int playerHealth = 0;
        foreach (var details in _playerDetail)
        {
            if (details.Key.GetID() == player.GetID())
            {
                playerHealth += details.Value.GetHealth();
            }
        }
        return playerHealth;
    }

    /// <summary>
    /// extracts the list of alive player, alive evaluated when hp greater than zero
    /// </summary>
    /// <returns>list of IPlayer that still in game and has hp greater than zero</returns>
    /// <exception cref="NullReferenceException"></exception>
    public List<IPlayer> GetAlivePlayers()
    {
        if (_playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Player Yet!");
        }

        List<IPlayer> alivePlayers = new();
        foreach (var details in _playerDetail)
        {
            int playerHealthPoint = details.Value.GetHealth();

            if (playerHealthPoint > 0)
            {
                alivePlayers.Add(details.Key);
            }
        }
        return alivePlayers;
    }

    /// <summary>
    /// extracts the count of alive player list
    /// </summary>
    /// <returns>integer representing players left</returns>
    public int PlayersLeft()
    {
        List<IPlayer> alivePlayers = GetAlivePlayers();
        return alivePlayers.Count;
    }
    
}
