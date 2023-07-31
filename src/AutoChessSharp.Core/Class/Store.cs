namespace AutoChessSharp.Core;

//* Core Store Mechanics, not yet implement reroll and lock
//* passing instances manually by method
//! key difference from the cd, use dict instead of list

public class Store
{
    private List<Piece> storePieces;
    private List<Piece> availPieces;

    //* store now has composite relation with Piece, list of Piece to be precise
    public Store(List<Piece> availPieces)
    {
        this.availPieces = availPieces;
    }

    // * LinQ implementation to populate storePieces
    public List<Piece> RerollStore()
    {
        Random random= new Random();

        List<Piece> rolledPieces = availPieces.OrderByDescending(p => random.Next()).ToList();

        //? fixed to take 5?
        storePieces = rolledPieces.Take(5).ToList();
        return storePieces;
    }


    //! Dependency Inversion warning: params uses Piece rather IPiece
    public int GetPrice(Piece piece)
    {
        if (storePieces == null)
        {
            throw new NullReferenceException(message: "Roll the store first!");
        }

        if (storePieces.Contains(piece))
        {
            return piece.GetPrice();
        }
        else
        {
            throw new Exception(message: "Piece is not available in current roll");
        }
    }

    public List<Piece> GetPrice()
    {
        return storePieces;
    }


    // ? seems Lock method is unnecessary
    // public void Lock()
    // {
    //     throw new NotImplementedException();

    //! method not mandatory
    // public bool AddPiece(Piece piece, int price)
    // {
    //     if (storePieces.ContainsKey(piece))
    //     {
    //         storePieces[piece] = price;
    //     }

    //     bool success = storePieces.TryAdd(piece, price);
    //     return success;
    // }
    // }

}
