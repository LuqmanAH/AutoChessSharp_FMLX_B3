namespace AutoChessSharp.Core;

public class PlayerInfo : IPlayerInfo
{
    private int _level;
    private int _experience;
    private int _health;
    private int _gold;
    // TODO : Player Pieces List
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

    // TODO: GetSet pieces
    // TODO: Setters

    public bool SetExperience(int Experience)
    {
        throw new NotImplementedException();
    }

    public bool SetHealth(int health)
    {
        throw new NotImplementedException();
    }

    public bool SetLevel(int level)
    {
        throw new NotImplementedException();
    }

    public bool SetGold(int gold)
    {
        throw new NotImplementedException();
    }
}
