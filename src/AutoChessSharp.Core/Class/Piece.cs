namespace AutoChessSharp.Core;
public class Piece : IPiece
{
    private IBoard board;
    private Position? _position;
    private string? _pieceName;
    private ArcheTypeEnum archeType;
    private int _healthPoint;
    private int _attack;

    public Piece(IBoard board, int archeType =1)
    {
        this.board = board;
        this.archeType = (ArcheTypeEnum)archeType;

    }

    public ArcheTypeEnum GetArcheTypeEnum()
    {
        return archeType;
    }

    public Position? GetPosition()
    {
        return _position;
    }

    public bool SetPosition(Position _position)
    {
        if (_position.GetX() < 0 || _position.GetY() < 0 || _position.GetX() > board.GetBoardSize() || _position.GetY() > board.GetBoardSize())
        {
            return false;
        }
        this._position = _position;
        return true;
    }
    
    public string? GetName()
    {
        return _pieceName;
    }

    public bool SetName(string _pieceName)
    {
        if (this._pieceName == _pieceName)
        {
            return false;
        }
        this._pieceName = _pieceName;
        return true;
    }

    public int GetHealthPoint()
    {
        return _healthPoint;
    }

    public bool SetHealthPoint(int _healthPoint)
    {
        if (_healthPoint < 0)
        {
            return false;
        }
        this._healthPoint = _healthPoint;
        return true;
    }

    public int GetAttack()
    {
        return _attack;
    }

    public bool SetAttack(int _attack)
    {
        if (_attack < 0)
        {
            return false;
        }
        this._attack = _attack;
        return true;
    }

    public bool IsHealthZero()
    {
        if (_healthPoint != 0)
        {
            return false;
        }
        return true;

    }
}
