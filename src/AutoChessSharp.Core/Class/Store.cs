namespace AutoChessSharp.Core;

//* Core Store Mechanics, not yet implement reroll and lock
//* passing instances manually by method
//! key difference from the cd, use dict instead of list

public class Store
{
    private Dictionary<IPiece, int> storePieces;
    private Dictionary<IPiece, int> availPieces;

    public Store()
    {
        storePieces = new Dictionary<IPiece,int>();
    }

    public bool AddPiece(IPiece piece, int price)
    {
        if (storePieces.ContainsKey(piece))
        {
            storePieces[piece] = price;
        }

        bool success = storePieces.TryAdd(piece, price);
        return success;
    }

    public int GetPrice(IPiece piece)
    {
        int price = 0;
        foreach (var key in storePieces.Keys)
        {
            if (key == piece)
            {
                price += storePieces[key];
            }
        }
        return price;
    }

    public Dictionary<IPiece, int> GetAllStoreItem()
    {
        return storePieces;
    }

    // TODO:
    public void Reroll()
    {
        throw new NotImplementedException();
    }

    // TODO:
    public void Lock()
    {
        throw new NotImplementedException();
    }
}
