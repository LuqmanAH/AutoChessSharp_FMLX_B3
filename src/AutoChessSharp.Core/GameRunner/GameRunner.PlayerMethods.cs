namespace AutoChessSharp.Core;

public partial class GameRunner
{
    public bool AddPlayer(IPlayer player)
    {
        bool addSuccess = _playerDetail.TryAdd(player, new PlayerInfo());
        return addSuccess;
    }
    
    public Dictionary<IPlayer, PlayerInfo> GetInGamePlayers()
    {
        return _playerDetail;
    }

    public IPlayer GetPlayer(int playerIndex)
    {
        return (Player)_playerDetail.Keys.ElementAt(playerIndex - 1);
    }

    //* buat separasi methods, sesuai dengan stat yang diinput user
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

    public int GetPlayerCurrentLevel(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetLevel();

    }
    
    public int GetPlayerCurrentExperience(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetExperience();

    }
    
    public int GetPlayerCurrentHP(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetHealth();

    }
    
    public int GetPlayerCurrentGold(IPlayer player)
    {
        PlayerInfo playerInfo = _playerDetail[player];
        return playerInfo.GetGold();

    }

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

    public int PlayersLeft()
    {
        List<IPlayer> alivePlayers = GetAlivePlayers();
        return alivePlayers.Count;
    }
    
}
