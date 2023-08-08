using System.Runtime.Serialization.Json;

namespace AutoChessSharp.Core;

public delegate void AfterClashEvent(SortedDictionary<int, IPlayer> clashResult); 

public partial class GameRunner
{
    private Dictionary<IPlayer, PlayerInfo> _playerDetail;
    private IBoard _board;
    private Store _store;
    private GameStatusEnum _gameStatus;
    private int _round;
    private int _countDown;
    private KeyValuePair<int, IPlayer> _clashLoser;
    private KeyValuePair<int, IPlayer> _clashWinzner;
    private AfterClashEvent afterClashEvent;

    //* ctor
    /// <summary>
    /// Represents the auto chess game runner class, shares composition with a board class
    /// </summary>
    /// <param name="board"></param>
    public GameRunner(IBoard board)
    {
        _round = 1;
        _board = board;
        _store = new Store();
        _store.RerollStore();
        _gameStatus = GameStatusEnum.NotStarted;
        _playerDetail = new Dictionary<IPlayer, PlayerInfo>();
        afterClashEvent= new(ClashLoserEvent);
        afterClashEvent += ClashWinnerEvent;
    }

    /// <summary>
    /// Attempt to populate store _available piece collection from external JSON data
    /// </summary>
    /// <param name="path"></param>
    /// <returns>true when path given is valid</returns>
    public bool SetStorePieces(string path)
    {
        try
        {
            List<Piece> storePieces = GetStorePiecesDB(path);
            _store.SetStorePieces(storePieces);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Deserializer to read from JSON serialized object
    /// </summary>
    /// <param name="path"></param>
    /// <returns>Piece collection</returns>
    private List<Piece> GetStorePiecesDB(string path)
    {
        var deserializer = new DataContractJsonSerializer(typeof(List<Piece>));

        using FileStream fileStream = new FileStream(path, FileMode.Open);
        List<Piece>? piecesToPlay = (List<Piece>?)deserializer.ReadObject(fileStream);
        return piecesToPlay;

    }

    //* Board and store getters

    /// <summary>
    /// Give access to store methods
    /// </summary>
    /// <returns>store associated to a game instance</returns>
    public Store GetStore()
    {
        return _store;
    }

    /// <summary>
    /// Give access to Board boundary
    /// </summary>
    /// <returns>Board associated to a game instance</returns>
    public IBoard GetBoard()
    {
        return _board;
    }

    //* Round mech

    /// <summary>
    /// Access current on going round in a game
    /// </summary>
    /// <returns>integer representing round</returns>
    public int GetCurrentRound()
    {
        return _round;
    }

    /// <summary>
    /// Attempts to set the round field of a game instance
    /// </summary>
    /// <param name="round"></param>
    /// <returns>true when the integer given is not negative value</returns>
    public bool SetRound(int round)
    {
        if (round < 0)
        {
            return false;
        }
        _round = round;
        return true;
    }

    /// <summary>
    /// Advances the game round, dispensing exp and gold to every player based on arguments
    /// </summary>
    /// <param name="expGiven"></param>
    /// <param name="goldGiven"></param>
    /// <returns>true when the countdown reached 0</returns>
    public bool GoNextRound(int expGiven, int goldGiven)
    {
        if (_countDown == 0)
        {
            _round ++;

            foreach (var playerInfos in _playerDetail.Values)
            {
                int exp = playerInfos.GetExperience();
                int gold = playerInfos.GetGold();

                exp += expGiven;
                gold += goldGiven;

                playerInfos.SetExperience(exp);
                playerInfos.SetGold(gold);
                playerInfos.IncrementLevel();
            }

            return true;
        }
        return false;
    }

    //* Countdown getter/setter
    /// <summary>
    /// Give access to countdown variable
    /// </summary>
    /// <returns>integer representing countdown</returns>
    public int GetCountDown()
    {
        return _countDown;
    }

    /// <summary>
    /// Set an integer as countdown timer for clash purpose, may be used for other purposes in the game flow
    /// </summary>
    /// <param name="countDown"></param>
    /// <returns></returns>
    public bool SetCountDown(int countDown)
    {
        if (countDown < 0)
        {
            return false;
        }
        _countDown = countDown;
        return true;
    }

    //* Game status getter/setter
    /// <summary>
    /// Give access to current status of the game
    /// </summary>
    /// <returns>game status enum</returns>
    public GameStatusEnum GetGameStatus()
    {
        return _gameStatus;
    }

    /// <summary>
    /// change the game status variable based on the passed argument
    /// </summary>
    /// <param name="gameStatus"></param>
    /// <returns>true when the passed argument is different from current status</returns>
    public bool SetGameStatus(GameStatusEnum gameStatus)
    {
        if (gameStatus == _gameStatus)
        {
            return false;
        }
        _gameStatus = gameStatus;
        return true;
    }

    //? include to player methods?
    /// <summary>
    /// Simulate the piece transaction process by the player
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns>true when the corresponding player gold sufficient</returns>
    public bool BuyFromStore(Player player, Piece piece)
    {
        
        int piecePrice = _store.GetPiecePrice(piece);
        int playerBalance = GetPlayerStats(player)["Gold"];

        int updatedBalance = playerBalance - piecePrice;
        bool buySuccess = GetInGamePlayers()[player].SetGold(updatedBalance);

        if (!buySuccess)
        {
            return false;
        }

        _playerDetail[player].SetPieceToList(piece);
        return true;
    }
    
}
