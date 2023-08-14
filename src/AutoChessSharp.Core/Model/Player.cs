namespace AutoChessSharp.Core;

/// <summary>
/// IPlayer basic implementation for auto chess game
/// </summary>
public class Player : IPlayer
{
    private string? _name;
    private static int _IDIncrement = 1;
    private int _playerId;

    /// <summary>
    /// Represents the auto chess game player object
    /// </summary>
    /// <param name="_name"></param>
    public Player(string? _name = null)
    {
        this._name = _name;
        _playerId = _IDIncrement;
        _IDIncrement ++;
    }

    /// <summary>
    /// Get the ID of the corresponding player instance
    /// </summary>
    /// <returns>integer representing the player ID</returns>
    public int GetID()
    {
        return _playerId;
    }

    /// <summary>
    /// Get the name of the corresponding player instance
    /// </summary>
    /// <returns>string representing player name when not null</returns>
    public string GetName()
    {
        if (_name != null)
        {
            return _name;
        }
        return string.Empty;
    }

    /// <summary>
    /// Attempts to manually override the incremental ID algorithm by setting ID with given custom integer
    /// </summary>
    /// <param name="_playerId"></param>
    /// <returns>true when the given integer is not already set as the ID of the player</returns>
    public bool SetID(int _playerId)
    {
        if (this._playerId != _playerId)
        {
            this._playerId = _playerId;
            return true;
        }
        return false;
        
    }

    /// <summary>
    /// Attempts to set the player name with given string
    /// </summary>
    /// <param name="_name"></param>
    /// <returns>true when the given string is not already set as the player name and not empty</returns>
    public bool SetPlayerName(string _name)
    {
        if (this._name == _name || _name == "")
        {
            return false;
        }
        this._name = _name;
        return true;
    }
}
