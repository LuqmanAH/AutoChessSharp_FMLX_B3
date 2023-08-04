using System.ComponentModel;

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
        _round = 1;
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

    public SortedDictionary<int, IPlayer> GameClash()
    {
        //* Inisiasi variable untuk clash
        SortedDictionary<int, IPlayer> afterClash = new SortedDictionary<int, IPlayer>();
        Random rng = new Random();

        // decouple algoritma: extract list of piece from pDetail
        List<Piece>[] eachPlayerPieces = GetEachPlayerPiece();

        // decouple algoritma: shuffle random list of piece
        List<Piece>[] shuffledPlayerPieces = PlayerPieceShuffle(eachPlayerPieces, rng);

        // decouple algoritma: extract random count from shuffled
        List<Piece>[] playerSurvivorPieces = SurvivorRandomExtract(shuffledPlayerPieces, rng, 3);

        // decouple algoritma: build the dictionary
        int[] playerSurvivorsCount = SurvivorsCount(playerSurvivorPieces);

        int survivorIndex = 0;
        foreach (var player in _playerDetail.Keys)
        {
            afterClash.Add(playerSurvivorsCount[survivorIndex], player);
            survivorIndex ++;
        }

        //* Returned value
        return afterClash;
    }

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

    private List<Piece>[] PlayerPieceShuffle(List<Piece>[] piecesToShuffle, Random rng)
    {
        List<Piece>[] shuffledPlayerPieces = new List<Piece>[PlayersLeft()];
        for (int playerID = 0; playerID < PlayersLeft(); playerID++)
        {
            shuffledPlayerPieces[playerID] = piecesToShuffle[playerID].OrderByDescending(playerID => rng.Next()).ToList();
        }
        return shuffledPlayerPieces;
    }

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
        return playerSurvivorPieces;
    }

    private int[] SurvivorsCount(List<Piece>[] survivorsToCount)
    {
        int[] playerSurvivorsCount = new int[PlayersLeft()];

        playerSurvivorsCount[0] = survivorsToCount[0].Count;
        playerSurvivorsCount[1] = survivorsToCount[1].Count;

        return playerSurvivorsCount;
    }

    public List<Piece> GetPlayersPiece(Player player)
    {
        List<Piece> playerPieces = new();
        foreach (var playerData in _playerDetail)
        {
            if (playerData.Key == player)
            {
                playerPieces = playerData.Value.GetPieces();
            }
        }
        return playerPieces;
    }

    public bool BuyFromStore(Player player, Piece piece)
    {
        //! if (!_store.Contains)
        //! Gold belum dikurangi

        _playerDetail[player].GetPieces().Add(piece);
        return true;
    }

    //TODO clash loser

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

    public int DecreasePlayerHealth(IPlayer player, List<Piece> piecesLeft)
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
