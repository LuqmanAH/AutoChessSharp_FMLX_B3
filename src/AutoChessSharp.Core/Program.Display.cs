using AutoChessSharp.Core;

partial class Program
{

    public static void InitPrompt(GameRunner gameRunner, Player player, int ID)
    {
        CleanScreen();
        DisplayHelper("====Auto Chess Game====");
        DisplayHelper("* This is the two players version of the game ");
        DisplayHelper($"* Enter string as player {ID} name: ");
        string? playerOneName = UserInputPrompt();
        bool checkOne = player.SetPlayerName(playerOneName);
        gameRunner.AddPlayer(player);

        while (!checkOne)
        {
            CleanScreen();
            DisplayHelper("Name cannot be set, please Input another: ");
            playerOneName = UserInputPrompt();
            checkOne = player.SetPlayerName(playerOneName);
        }

        CleanScreen();
        DisplayHelper("Name successfully set, press any key to continue..");
        UserInputPrompt();
    }

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
            DisplayHelper($"{storeStock.IndexOf(piece)+ 1} ] {piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()} for {piece.GetPrice()} Gold\nstats: ATK: {piece.GetAttack()} HP: {piece.GetHealthPoint()}\n");
        }
    }
}
