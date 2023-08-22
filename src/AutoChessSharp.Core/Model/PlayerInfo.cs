
//? method for setting the piece list is kinda wacky, might review when store complete

namespace AutoChessSharp.Core;

/// <summary>
/// IPlayerInfo basic implementation for auto chess game
/// </summary>
public class PlayerInfo : IPlayerInfo
{
    private int _level;
    LevelInfoEnum maxLevel = LevelInfoEnum.MaxLevel;
    LevelInfoEnum minLevel = LevelInfoEnum.MinLevel;

    private int _experience;
    ExperienceInfoEnum maxExp = ExperienceInfoEnum.MaxExperience;
    ExperienceInfoEnum minExp = ExperienceInfoEnum.MinExperience;

    private int _health;
    HealthInfoEnum maxHealth = HealthInfoEnum.MaxHealth;
    HealthInfoEnum minHealth = HealthInfoEnum.MinHealth;

    private int _gold;
    private int _requiredExperience = 1;

    private List<IPiece>? _pieceOwned;
    private List<IPiece>? _pieceOnField;

    /// <summary>
    /// Represents the auto chess player info, set starting gold to 5 by default
    /// </summary>
    /// <param name="_gold"></param>
    public PlayerInfo(int _gold = 5)
    {
        _level = (int)LevelInfoEnum.MinLevel;
        _experience = (int)ExperienceInfoEnum.MinExperience;
        _health = (int)HealthInfoEnum.MaxHealth;
        _pieceOwned = new List<IPiece>();
        _pieceOnField = new List<IPiece>();
        this._gold = _gold;
    }

    /// <summary>
    /// Get the experience value of the corresponding player info instance
    /// </summary>
    /// <returns>integer representing the experience of player info</returns>
    public int GetExperience()
    {
        return _experience;
    }

    /// <summary>
    /// Get the health point value of the corresponding player info instance
    /// </summary>
    /// <returns>integer representing the health point of player info</returns>
    public int GetHealth()
    {
        return _health;
    }

    /// <summary>
    /// Get the level value of the corresponding player info instance
    /// </summary>
    /// <returns>integer representing the level of player info</returns>
    public int GetLevel()
    {
        return _level;
    }
    
    /// <summary>
    /// Get the gold balance of the corresponding player info instance
    /// </summary>
    /// <returns>integer representing the gold balance of player info</returns>
    public int GetGold()
    {
        return _gold;
    }

    /// <summary>
    /// Get the list of pieces owned by the player info, throws exception when the field is null
    /// </summary>
    /// <returns>List of pieces representing the pieces owned by the player info</returns>
    /// <exception cref="NullReferenceException"></exception>
    public List<IPiece> GetOwnedPieces()
    {
        if (_pieceOwned == null)
        {
            throw new NullReferenceException(message: "No Piece is owned by player!");
        }
        return _pieceOwned;
    }

    public IPiece GetOwnedPiece(IPiece piece)
    {
        if (_pieceOwned == null)
        {
            throw new NullReferenceException(message: "No Piece is owned by player!");
        }

        if (_pieceOwned.Contains(piece))
        {
            return piece;
        }
        return default;
    }

    public List<IPiece> GetOnFieldPieces()
    {
        if (_pieceOnField == null)
        {
            throw new NullReferenceException(message: "No Piece is set on field!");
        }
        return _pieceOnField;
    }

    /// <summary>
    /// Attempts to set the experience value of the corresponding player info instance
    /// </summary>
    /// <param name="_experience"></param>
    /// <returns>true when the value is within the limit of allowable experience</returns>
    public bool SetExperience(int _experience)
    {
        if (_experience < (int)minExp || _experience > (int)maxExp)
        {
            return false;
        }
        this._experience = _experience;
        return true;
    }

    /// <summary>
    /// Attempts to set the health point of the corresponding player info instance
    /// </summary>
    /// <param name="_health"></param>
    /// <returns>true when the value is within the limit of allowable health point</returns>
    public bool SetHealth(int _health)
    {
        if (_health < (int)minHealth || _health > (int)maxHealth)
        {
            return false;
        }
        this._health = _health;
        return true;
    }

    /// <summary>
    /// Attempts to set the level of the corresponding player info instance
    /// </summary>
    /// <param name="_level"></param>
    /// <returns>true when the value is within the limit of allowable level</returns>
    public bool SetLevel(int _level)
    {
        if (_level < (int)minLevel || _level > (int)maxLevel)
        {
            return false;
        }
        this._level = _level;
        return true;
    }

    /// <summary>
    /// attempts to set the gold balance of the corresponding player info instance
    /// </summary>
    /// <param name="_gold"></param>
    /// <returns>true when the value is within limit of the allowable gold balance</returns>
    public bool SetGold(int _gold)
    {
        if (_gold < 0)
        {
            return false;
        }
        this._gold = _gold;
        return true;
    }

    /// <summary>
    /// attempts to set list of pieces collection to the corresponding player info instance
    /// </summary>
    /// <param name="pieceList"></param>
    /// <returns>true when the given collection is not null</returns>
    public bool SetOwnedPieces (List<IPiece>? pieceList)
    {
        if (pieceList is null)
        {
            return false;
        }
        _pieceOwned = pieceList;
        return true;
    }

    /// <summary>
    /// attempts to set a single instance of Piece to the existing list of pieces
    /// </summary>
    /// <param name="piece"></param>
    /// <returns>true when the list of pieces is not null and the given piece is not null</returns>
    public bool SetPieceToOwned(AutoChessPiece piece)
    {
        if (_pieceOwned is null || piece is null)
        {
            return false;
        }
        _pieceOwned.Add(piece);
        return true;
    }

    public bool SetPieceOnField(AutoChessPiece piece, Position position)
    {
        if (!_pieceOwned.Contains(piece) || !piece.SetPosition(position))
        {
            return false;
        }
        piece.SetPosition(position);
        _pieceOnField.Add(piece);
        return true;
    }

    /// <summary>
    /// evaluates the experience, then attempts to increment the level of the player info
    /// </summary>
    /// <returns>true when the experience reached the required value</returns>
    public bool IncrementLevel()
    {
        if (_experience == _requiredExperience)
        {
            _level ++;
            _requiredExperience ++;
            return true;
        }
        return false;
    }
}
