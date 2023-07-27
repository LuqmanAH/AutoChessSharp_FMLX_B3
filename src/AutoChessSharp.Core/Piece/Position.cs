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

    public bool SetX(int x)
    {
        if (x > 0)
        {
            _x = x;
            return true;
        }
        return false;
    }

    public bool SetY(int y)
    {
        if (y > 0)
        {
            _y = y;
            return true;
        }
        return false;
    }
}
