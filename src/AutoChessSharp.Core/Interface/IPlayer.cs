namespace AutoChessSharp.Core;

/// <summary>
/// Provides base structure for classes associated with game Player
/// </summary>
public interface IPlayer
{
    public string GetName();
    public bool SetPlayerName(string name);
    public int GetID();
    public bool SetID(int ID);
}
