namespace AutoChessSharp.Core;

/// <summary>
/// Provides base structure for classes associated with game board
/// </summary>
public interface IBoard
{
    public int GetBoardSize();
    public bool SetBoardSize(int size);
}
