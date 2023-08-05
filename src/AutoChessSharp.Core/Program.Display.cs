using AutoChessSharp.Core;

partial class Program
{
    public static void DisplayHelper<T>(T value)
    {
        Console.WriteLine(value);
    }

    public static void InlineDisplayHelper<T>(T value)
    {
        Console.Write(value);
    }

    public static void CleanScreen()
    {
        Console.Clear();
    }

    //! Unused
    public static char Choose()
    {
        var userChoice = Console.ReadKey().KeyChar;
        return userChoice;
    }

    public static string? UserInputPrompt()
    {
        string? userInput = Console.ReadLine();
        return userInput;
    }

    public static void ShowPlayerStats(GameRunner autoChessGame, Player player1)
    {
        foreach (var stat in autoChessGame.GetPlayerStats(player1))
        {
            DisplayHelper($"{stat.Key} = {stat.Value}");
        }
    }

    public static void ShowStoreStock(List<Piece> storeStock)
    {
        foreach (var piece in storeStock)
        {
            DisplayHelper($"{storeStock.IndexOf(piece)+ 1} ] {piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()} for {piece.GetPrice()} Gold");
        }
    }
}
