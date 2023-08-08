
//? method for setting the piece list is kinda wacky, might review when store complete

namespace AutoChessSharp.Core;

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

    private List<Piece>? _pieceList;

    public PlayerInfo(int _gold = 5)
    {
        _level = (int)LevelInfoEnum.MinLevel;
        _experience = (int)ExperienceInfoEnum.MinExperience;
        _health = (int)HealthInfoEnum.MaxHealth;
        _pieceList = new List<Piece>();
        this._gold = _gold;
    }

    public int GetExperience()
    {
        return _experience;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetLevel()
    {
        return _level;
    }
    
    public int GetGold()
    {
        return _gold;
    }

    
    public List<Piece> GetPieces()
    {
        if (_pieceList == null)
        {
            throw new NullReferenceException(message: "No Piece is set to player!");
        }
        return _pieceList;
    }

    public bool SetExperience(int _experience)
    {
        if (_experience < (int)minExp || _experience > (int)maxExp)
        {
            return false;
        }
        this._experience = _experience;
        return true;
    }

    public bool SetHealth(int _health)
    {
        if (_health < (int)minHealth || _health > (int)maxHealth)
        {
            return false;
        }
        this._health = _health;
        return true;
    }

    public bool SetLevel(int _level)
    {
        if (_level < (int)minLevel || _level > (int)maxLevel)
        {
            return false;
        }
        this._level = _level;
        return true;
    }

    public bool SetGold(int _gold)
    {
        if (_gold < 0)
        {
            return false;
        }
        this._gold = _gold;
        return true;
    }

    public bool SetPieces (List<Piece>? pieceList)
    {
        if (pieceList is null)
        {
            return false;
        }
        _pieceList = pieceList;
        return true;
    }

    public bool SetPieceToList(Piece piece)
    {
        if (_pieceList is null || piece is null)
        {
            return false;
        }
        _pieceList.Add(piece);
        return true;
    }

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
