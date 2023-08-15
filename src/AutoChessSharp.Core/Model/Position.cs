using System.Runtime.Serialization;

namespace AutoChessSharp.Core;

[DataContract]
public class Position
{
    [DataMember(Name = "_x")]
    private int _x;

    [DataMember(Name = "_y")]
    private int _y;


    public Position()
    {
        _x = 0;
        _y = 0;
    }

    public Position(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public bool SetX(int _x)
    {
        if (_x < 0)
        {
            return false;
        }
        this._x = _x;
        return true;
    }

    public bool SetY(int _y)
    {
        if (_y < 0)
        {
            return false;
        }
        this._y = _y;
        return true;
    }
}
