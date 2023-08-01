namespace AutoChessSharp.Core;

public class Store
{
    private List<Piece>? _storePieces;
    private List<Piece> _availPieces;
    private bool _isRollable = true;

    //* store now has composite relation with Piece, list of Piece to be precise
    public Store(List<Piece> availPieces)
    {
        _availPieces = availPieces;
    }

    // * LinQ implementation to populate storePieces
    public List<Piece> RerollStore()
    {
        if (!_isRollable)
        {
            throw new Exception(message: "You have locked your store!");
        }
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

    public List<Piece> GetFromStore()
    {
        if (_storePieces == null)
        {
            throw new NullReferenceException(message: "Store piece is not rolled yet!");
        }
        return _storePieces;
    }

    // ? seems Lock method is unnecessary, bingung implementasinya

    // allow user to lock current roll for the next round
    public bool LockStore()
    {
        if (_isRollable == true)
        {
            _isRollable = false;
            return true;
        }
        return false;
    }

    // program should automatically unlock after the lock round
    public bool UnlockStore()
    {
        if (_isRollable == false)
        {
            _isRollable = true;
            return true;
        }
        return false;
    }

}
