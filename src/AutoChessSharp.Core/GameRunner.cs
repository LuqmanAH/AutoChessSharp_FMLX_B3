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
    public int GetCurrentRound()
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

    public bool GoNextRound()
    {
        if (_countDown == 0)
        {
            _round ++;
            return true;
        }
        return false;
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

    public bool SetGameStatus(GameStatusEnum gameStatus)
    {
        if (gameStatus == _gameStatus)
        {
            return false;
        }
        _gameStatus = gameStatus;
        return true;
    }

    //TODO: Many things sadge

    public Dictionary<IPlayer, int> GameClash()
    {
        List<Piece>? player1Pieces = new();
        List<Piece>? player2Pieces = new();
        
        Player player1 = new();
        Player player2 = new();
        Random random= new Random();

        //! masih salah brow
        foreach (var playerData in _playerDetail)
        {
            PlayerInfo player1Info = playerData.Value;

            player1Pieces = player1Info.GetPieces();

            player1 = (Player)playerData.Key;
            player1 = (Player)playerData.Key;
        }
        //!

        if (player1Pieces.Count == 0 && player2Pieces.Count == 0)
        {
            throw new Exception(message: "No players placed their pieces!");
        }

        int player1Survivors = random.Next(0, 5);
        int player2Survivors = random.Next(0, 5);

        List<Piece> player1Survivor = new List<Piece>();
        List<Piece> player2Survivor = new List<Piece>();

        player1Pieces = player1Pieces.OrderByDescending(player1Pieces => random.Next()).ToList();
        player2Pieces = player2Pieces.OrderByDescending(player2Pieces => random.Next()).ToList();

        player1Survivor = player1Pieces.Take(player1Survivors).ToList();
        player2Survivor = player2Pieces.Take(player2Survivors).ToList();

        int player1SurvivorCount = player1Survivor.Count();
        int player2SurvivorCount = player2Survivor.Count();
        
        Dictionary<IPlayer, int> afterClashResult = new Dictionary<IPlayer, int>();
        afterClashResult.Add(player1, player1SurvivorCount);
    }

    public int DecreasePlayerHealth(IPlayer player, List<Piece> piecesLeft)
    {
        throw new NotImplementedException();
    }
    public bool OnTerritory(Position position)
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
