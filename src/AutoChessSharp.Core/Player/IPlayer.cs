namespace AutoChessSharp.Core;

public interface IPlayer
{
    public string GetName();
    public bool SetString(string name);
    public int GetID();
    public bool SetID(int ID);
}
