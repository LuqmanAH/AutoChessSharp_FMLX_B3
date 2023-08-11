using System.Runtime.Serialization;

namespace AutoChessSharp.Core;

/// <summary>
/// IPiece basic implementation for auto chess game
/// </summary>
[DataContract(Name = "Piece")]
public class AutoChessPiece : IPiece
{
    [DataMember(Name="_archeType")]
    private ArcheTypeEnum _archeType;

    [DataMember(Name="_rarity")]
    private int _rarity;

    [DataMember(Name="_position")]
    private Position? _position;

    [DataMember(Name="_price")]
    private int _price;

    [DataMember(Name="_pieceName")]
    private string? _pieceName;

    [DataMember(Name = "_healthPoint")]
    private int _healthPoint;

    [DataMember(Name = "_attack")]
    private int _attack;

    /// <summary>
    /// Represents the auto chess game pieces, must define arche type and rarity
    /// </summary>
    /// <param name="archeType"></param>
    /// <param name="rarity"></param>
    /// <param name="pieceName"></param>
    public AutoChessPiece(ArcheTypeEnum archeType, RarityEnum rarity, string? pieceName = null)
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

    /// <summary>
    /// Get the position object of the corresponding piece
    /// </summary>
    /// <returns>Position object indicating piece current position</returns>
    /// <exception cref="NullReferenceException"></exception>
    public Position GetPosition()
    {
        if (_position == null)
        {
            throw new NullReferenceException(message:"Position unset!");
        }
        return _position;
    }
    
    /// <summary>
    /// Get the defined name of the piece returns null when name not set
    /// </summary>
    /// <returns>string representing piece name</returns>
    public string? GetName()
    {
        return _pieceName;
    }

    /// <summary>
    /// Attempts to set current piece name by the given string
    /// </summary>
    /// <param name="_pieceName"></param>
    /// <returns>true when the given string is not already set as the piece name</returns>
    public bool SetName(string _pieceName)
    {
        if (this._pieceName == _pieceName)
        {
            return false;
        }
        this._pieceName = _pieceName;
        return true;
    }

    /// <summary>
    /// Get the HP of the corresponding piece
    /// </summary>
    /// <returns>integer representing health of the piece</returns>
    public int GetHealthPoint()
    {
        return _healthPoint;
    }

    /// <summary>
    /// Attempts to set the health of corresponding piece by the given value
    /// </summary>
    /// <param name="_healthPoint"></param>
    /// <returns>true when the given value greater than or equal 0</returns>
    public bool SetHealthPoint(int _healthPoint)
    {
        if (_healthPoint < 0)
        {
            return false;
        }
        this._healthPoint = _healthPoint;
        return true;
    }

    /// <summary>
    /// Get the ATK stat of the corresponding piece
    /// </summary>
    /// <returns>integer representing attack of the piece</returns>
    public int GetAttack()
    {
        return _attack;
    }

    /// <summary>
    /// Attempts to set the attack stat of corresponding piece by the given value
    /// </summary>
    /// <param name="_attack"></param>
    /// <returns>true when the given value greater than or equal 0</returns>
    public bool SetAttack(int _attack)
    {
        if (_attack < 0)
        {
            return false;
        }
        this._attack = _attack;
        return true;
    }

    /// <summary>
    /// Evaluates whether or not the piece has health point of 0
    /// </summary>
    /// <returns>true when the health point reaches zero</returns>
    public bool IsHealthZero()
    {
        if (_healthPoint != 0)
        {
            return false;
        }
        return true;

    }

    /// <summary>
    /// Get the price of a corresponding piece
    /// </summary>
    /// <returns>integer representing the piece price</returns>
    public int GetPrice()
    {
        return _price;
    }

    /// <summary>
    /// Get the arche type of the piece from the arche type enum
    /// </summary>
    /// <returns>ArcheTypeEnum constant</returns>
    public ArcheTypeEnum GetArcheType()
    {
        return _archeType;
    }

    /// <summary>
    /// Get the rarity of the piece from the rarity enum
    /// </summary>
    /// <returns>RarityEnum constant</returns>
    public RarityEnum GetRarityEnum()
    {
        return (RarityEnum)_rarity;
    }
}
