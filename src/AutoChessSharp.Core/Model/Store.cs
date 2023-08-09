namespace AutoChessSharp.Core;

/// <summary>
/// Class to simulate store behavior in auto chess game
/// </summary>
public class Store
{
    private List<Piece>? _storePieces;
    private List<Piece> _availPieces;

    //* store now has composite relation with Piece, list of Piece to be precise
    /// <summary>
    /// Represents the auto chess store, initially has empty list of available pieces
    /// </summary>
    public Store()
    {
        _availPieces = new List<Piece>();
    }

    // * LinQ implementation to populate storePieces
    /// <summary>
    /// Randomly roll the available pieces, then set 5 items to the store pieces
    /// </summary>
    /// <returns>list of pieces representing the store pieces</returns>
    public List<Piece> RerollStore()
    {
        Random random= new Random();

        List<Piece> rolledPieces = _availPieces.OrderByDescending(p => random.Next()).ToList();

        //? fixed to take 5?
        _storePieces = rolledPieces.Take(5).ToList();
        return _storePieces;
    }

    /// <summary>
    /// get the price of the given piece in the store
    /// </summary>
    /// <param name="piece"></param>
    /// <returns>integer representing price when the store has been rolled and contains the piece</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="Exception"></exception>
    public int GetPiecePrice(Piece piece)
    {
        if (_storePieces == null)
        {
            throw new NullReferenceException(message: "Roll the store first!");
        }

        if (_storePieces.Contains(piece))
        {
            return piece.GetPrice();
        }
        else
        {
            throw new Exception(message: "Piece is not available in current roll");
        }
    }

    /// <summary>
    /// get the store pieces without rerolling the store
    /// </summary>
    /// <returns>list of pieces representing the store stock when the store has been rolled at least once</returns>
    /// <exception cref="NullReferenceException"></exception>
    public List<Piece> GetStoreStock()
    {
        if (_storePieces == null)
        {
            throw new NullReferenceException(message: "Store piece is not rolled yet!");
        }
        return _storePieces;
    }

    /// <summary>
    /// attempts to set list of pieces to the store pieces fields
    /// </summary>
    /// <param name="piecesList"></param>
    /// <returns>true when the given list of pieces is not null</returns>
    public bool SetStorePieces(List<Piece> piecesList)
    {
        if (piecesList == null)
        {
            return false;
        }
        _availPieces = piecesList;
        return true;
    }

}
