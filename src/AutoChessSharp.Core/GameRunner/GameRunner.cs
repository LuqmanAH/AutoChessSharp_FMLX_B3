namespace AutoChessSharp.Core;
public partial class GameRunner
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

    //* Round mech

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

            foreach (var playerInfos in _playerDetail.Values)
            {
                int exp = playerInfos.GetExperience();
                int gold = playerInfos.GetGold();

                exp += 1;
                gold += 2;

                playerInfos.SetExperience(exp);
                playerInfos.SetGold(gold);
                playerInfos.IncrementLevel();
            }

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
        if (countDown < 0)
        {
            return false;
        }
        _countDown = countDown;
        return true;
    }

    //* Game status getter/setter
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

    //? include to player methods?
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
