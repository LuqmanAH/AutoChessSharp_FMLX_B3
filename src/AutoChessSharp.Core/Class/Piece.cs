namespace AutoChessSharp.Core;

// might not be a good idea to create IBoard here SOLVE
//? can the price implemented as a field here?
//? which better? implement positioning method here or in the GR?

public class Piece : IPiece
{
    private ArcheTypeEnum _archeType;
    private int _rarity;

    private Position? _position;
    private int _price;

    private string? _pieceName;
    private int _healthPoint;
    private int _attack;

    public Piece(ArcheTypeEnum archeType, RarityEnum rarity, string? pieceName = null)
    {
        _archeType = archeType;
        _rarity = (int)rarity;
        _pieceName = pieceName;
        _position = new Position(0,0);

        switch (archeType)
        {
            case ArcheTypeEnum.Warrior:
                _attack = 12 * _rarity;
                _healthPoint = 15 * _rarity;
                _price = 1 * _rarity;
                break;

            case ArcheTypeEnum.Mage:
                _attack = 18 * _rarity;
                _healthPoint = 5 * _rarity;
                _price = 2 * _rarity;
                break;

            case ArcheTypeEnum.Hunter:
                _attack = 15 * _rarity;
                _healthPoint = 6 * _rarity;
                _price = 1 * _rarity;
                break;

            case ArcheTypeEnum.Assassin:
                _attack = 16 * _rarity;
                _healthPoint = 10 * _rarity;
                _price = 3 * _rarity;
                break;

        }

    }

    public Position GetPosition()
    {
        if (_position == null)
        {
            throw new NullReferenceException(message:"Position unset!");
        }
        return _position;
    }

    //* position setting applied in GR, piece position should zero until in game
    // public bool SetPosition(Position _position)
    // {
    //     if (_position.GetX() < 0 || _position.GetY() < 0)
    //     {
    //         return false;
    //     }
    //     this._position = _position;
    //     return true;
    // }
    
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

    public int GetPrice()
    {
        return _price;
    }

    //* below methods might not be used
    public ArcheTypeEnum GetArcheType()
    {
        return _archeType;
    }

    public RarityEnum GetRarityEnum()
    {
        return (RarityEnum)_rarity;
    }
}
