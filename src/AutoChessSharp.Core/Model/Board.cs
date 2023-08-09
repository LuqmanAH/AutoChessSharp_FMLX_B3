namespace AutoChessSharp.Core;

/// <summary>
/// Class implementation of IBoard to provide game basic boundaries
/// </summary>
public class Board : IBoard
{
    private int _size;

    /// <summary>
    /// Represents the game board class, provides zero boundary by default
    /// </summary>
    /// <param name="size"></param>
    public Board (int size = 0)
    {
        _size = size;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns>returns integer representing the size of the board</returns>
    public int GetBoardSize()
    {
        return _size;
    }

    /// <summary>
    /// Use to set the board size by the given value
    /// </summary>
    /// <param name="size"></param>
    /// <returns>true when the given value is greater than zero</returns>
    public bool SetBoardSize(int size)
    {
        if (size > 0 )
        {
            _size = size;
            return true;
        }
        return false;
    }
}
