namespace AutoChessSharp.Core;
public class GameRunner
{
    private Dictionary<IPlayer, PlayerInfo> _playerDetail;
    private IBoard _board;
    private Store _store;
    private GameStatusEnum _gameStatus;
    private int _round;
    private int _countDown;

    //* ctor
    public GameRunner(IBoard board, Store store)
    {
        _round = 0;
        _board = board;
        _store = store;
        _gameStatus = GameStatusEnum.NotStarted;
        _playerDetail = new Dictionary<IPlayer, PlayerInfo>();
    }

    //* Board and store getters
    public Store GetStore()
    {
        return _store;
    }

    public IBoard GetBoard()
    {
        return _board;
    }

    //* Round getter/setter
    public int GetRound()
    {
        return _round;
    }

    public bool SetRound(int round)
    {
        if (round < 0)
        {
            return false;
        }
        _round = round;
        return true;
    }

    //* Countdown getter/setter
    public int GetCountDown()
    {
        return _countDown;
    }

    public bool SetCountDown(int countDown)
    {
        if (countDown <= 0)
        {
            return false;
        }
        _countDown = countDown;
        return true;
    }

    //* Game status getter/ (setter?)

    public GameStatusEnum GetGameStatus()
    {
        return _gameStatus;
    }

    //TODO: Many things sadge

    public List<int> CurrentRound()
    {
        throw new NotImplementedException();
    }

    public Dictionary<IPlayer, List<int>> GameClash()
    {
        throw new NotImplementedException();
    }

    public bool OnTerritory(Position position)
    {
        throw new NotImplementedException();
    }

    public bool GoNextRound(int x, int y)
    {
        throw new NotImplementedException();
    }

    public int DecreasePlayerHealth(IPlayer player, List<Piece> piecesLeft)
    {
        throw new NotImplementedException();
    }

    public bool GameOver()
    {
        throw new NotImplementedException();

    }

    public IPlayer CheckWinner()
    {
        throw new NotImplementedException();
    }

    //* Player methods
    public bool AddPlayer(IPlayer player)
    {
        bool addSuccess = _playerDetail.TryAdd(player, new PlayerInfo());
        return addSuccess;
    }

    public Dictionary<IPlayer, PlayerInfo> GetAllPlayers()
    {
        return _playerDetail;
    }

    //! different return type from cd
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
