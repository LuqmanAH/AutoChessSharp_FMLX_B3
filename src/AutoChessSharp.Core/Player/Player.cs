namespace AutoChessSharp.Core;

//? Guid Class is possible for ID implementation
//? but requires string as the field type

public class Player : IPlayer
{
    private string? _name;
    private static int _IDIncrement = 1;
    private int _playerId;

    //* attempt to make the ctor flexible but not overloading
    public Player(string? name = null)
    {
        _name = name;
        _playerId = _IDIncrement;
        _IDIncrement ++;
    }

    public int GetID()
    {
        return _playerId;
    }

    public string GetName()
    {
        if (_name != null)
        {
            return _name;
        }
        return "null";
    }

    public bool SetID(int _playerId)
    {
        if (this._playerId != _playerId)
        {
            this._playerId = _playerId;
            return true;
        }
        return false;
        
    }

    public bool SetPlayerName(string _name)
    {
        if (this._name == _name)
        {
            return false;
        }
        this._name = _name;
        return true;
    }
}
