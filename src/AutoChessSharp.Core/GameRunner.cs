using System.ComponentModel;

namespace AutoChessSharp.Core;
public class GameRunner
{
    private Dictionary<IPlayer, PlayerInfo> _playerDetail;
    private int _playerCount;
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
        _playerCount = _playerDetail.Keys.Count();
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
        //* Inisiasi variable untuk clash
        Dictionary<IPlayer, int> afterClash = new Dictionary<IPlayer, int>();
        Random rng = new Random();

        //TODO decouple algoritma: extract list of piece from pDetail
        List<Piece>[] eachPlayerPieces = new List<Piece>[_playerCount];
        foreach (var playerData in _playerDetail.Values)
        {
            for (int playerID = 0; playerID < _playerCount; playerID++)
            {
                if (eachPlayerPieces[playerID] is null)
                {
                    eachPlayerPieces[playerID] = playerData.GetPieces();
                }
            }
        }

        //TODO decouple algoritma: shuffle random list of piece
        List<Piece>[] shuffledPlayerPieces = new List<Piece>[_playerCount];
        for (int playerID = 0; playerID < _playerCount; playerID++)
        {
            shuffledPlayerPieces[playerID] = eachPlayerPieces[playerID].OrderByDescending(playerID => rng.Next()).ToList();
        }

        //TODO decouple algoritma: extract random count from shuffled
        List<Piece>[] playerSurvivorPieces = new List<Piece>[_playerCount];
        for (int playerID = 0; playerID < _playerCount; playerID++)
        {
            playerSurvivorPieces[playerID] = shuffledPlayerPieces[playerID].Take(rng.Next(0, 3)).ToList();
        }

        //TODO decouple algoritma: build the dictionary
        int[] playerSurvivorsCount = new int[_playerCount];

        playerSurvivorsCount[0] = playerSurvivorPieces[0].Count;
        playerSurvivorsCount[1] = playerSurvivorPieces[1].Count;
        int survivorIndex = 0;

        foreach (var player in _playerDetail.Keys)
        {
            afterClash.Add(player, playerSurvivorsCount[survivorIndex]);
            survivorIndex ++;
        }

        //* Returned value
        return afterClash;
    }

    private List<Piece>[] PlayerPieceExtract()
    {
        List<Piece>[] eachPlayerPieces = new List<Piece>[_playerCount];
        foreach (var playerData in _playerDetail.Values)
        {
            for (int playerID = 0; playerID < _playerCount; playerID++)
            {
                if (eachPlayerPieces[playerID] is null)
                {
                    eachPlayerPieces[playerID] = playerData.GetPieces();
                }
            }
        }
        return eachPlayerPieces;
    }

    private List<Piece>[] PlayerPieceShuffle(List<Piece>[] piecesToShuffle, Random rng)
    {
        List<Piece>[] shuffledPlayerPieces = new List<Piece>[_playerCount];
        for (int playerID = 0; playerID < _playerCount; playerID++)
        {
            shuffledPlayerPieces[playerID] = piecesToShuffle[playerID].OrderByDescending(playerID => rng.Next()).ToList();
        }
        return shuffledPlayerPieces;
    }

    private List<Piece>[] SurvivorRandomExtract(List<Piece>[] survivorsToExtract, Random rng, int maxAmount)
    {
        List<Piece>[] playerSurvivorPieces = new List<Piece>[_playerCount];
        for (int playerID = 0; playerID < _playerCount; playerID++)
        {
            playerSurvivorPieces[playerID] = survivorsToExtract[playerID].Take(rng.Next(0, maxAmount)).ToList();
        }
        return playerSurvivorPieces;
    }

    private int[] SurvivorsCount(List<Piece>[] survivorsToCount)
    {
        int[] playerSurvivorsCount = new int[_playerCount];

        playerSurvivorsCount[0] = survivorsToCount[0].Count;
        playerSurvivorsCount[1] = survivorsToCount[1].Count;

        return playerSurvivorsCount;
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
