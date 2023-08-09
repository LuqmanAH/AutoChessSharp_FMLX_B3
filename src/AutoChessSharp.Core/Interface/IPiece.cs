namespace AutoChessSharp.Core;

/// <summary>
/// Provides base structure for classes associated with game Piece
/// </summary>
public interface IPiece
{
    public string? GetName();
    public bool SetName(string name);
}
