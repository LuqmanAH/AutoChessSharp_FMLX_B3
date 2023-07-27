namespace AutoChessSharp.Core;

public interface IPlayer
{
    public string GetName();
    public bool SetPlayerName(string name);
    public int GetID();
    public bool SetID(int ID);
}
