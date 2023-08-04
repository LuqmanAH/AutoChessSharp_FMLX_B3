namespace AutoChessSharp.Core;

public class Store
{
    private List<Piece>? _storePieces;
    private List<Piece> _availPieces;

    //* store now has composite relation with Piece, list of Piece to be precise
    public Store(List<Piece>? availPieces)
    {
        _availPieces = availPieces ?? throw new NullReferenceException(message:"The Available Pieces are not set!");
    }

    // * LinQ implementation to populate storePieces
    public List<Piece> RerollStore()
    {
        Random random= new Random();

        List<Piece> rolledPieces = _availPieces.OrderByDescending(p => random.Next()).ToList();

        //? fixed to take 5?
        _storePieces = rolledPieces.Take(5).ToList();
        return _storePieces;
    }

    public int GetFromStore(Piece piece)
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

    public List<Piece> GetStoreStock()
    {
        if (_storePieces == null)
        {
            throw new NullReferenceException(message: "Store piece is not rolled yet!");
        }
        return _storePieces;
    }

}
