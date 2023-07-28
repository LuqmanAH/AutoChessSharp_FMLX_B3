namespace AutoChessSharp.Core;

public class Position
{
    private int _x;
    private int _y;

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
