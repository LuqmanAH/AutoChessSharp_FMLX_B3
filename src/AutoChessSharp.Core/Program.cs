using AutoChessSharp.Core;
using System.Runtime.Serialization.Json;

class Program
{
    public static void Main()
    {
        List<Piece>? piecesToPlay = PieceInitializer();
        Store? storeToPlay = new Store(piecesToPlay);
        storeToPlay.RerollStore();

        Board autoChessBoard = new Board(8);
        GameRunner autoChessGame = new GameRunner(autoChessBoard, storeToPlay);
        
        CleanScreen();
        Player[] players = new Player[2]
        {
            new Player(),
            new Player()
        };

        foreach (Player player in players)
        {
            InitPrompt(autoChessGame, player, player.GetID());
        }

        Player player1 = players[0];
        Player player2 = players[1];

        Dictionary<IPlayer, PlayerInfo> playerInGame = autoChessGame.GetInGamePlayers();

        CleanScreen();
        foreach (var playerData in playerInGame)
        {
            IPlayer player = playerData.Key;
            DisplayHelper($"{player.GetName()} is in game");
        }
        DisplayHelper("Let The Game Begins.. When you're ready (Press any key)");
        UserInputPrompt();

        CleanScreen();
        autoChessGame.SetGameStatus(GameStatusEnum.Ongoing);

        while (autoChessGame.GetGameStatus() == GameStatusEnum.Ongoing)
        {

            DisplayHelper($"==== Beginning Round {autoChessGame.GetCurrentRound()} ====\n");
            DisplayHelper($"player {player1.GetName()} stats:");
            foreach (var stat in autoChessGame.GetPlayerStats(players[0]))
            {
                DisplayHelper($"{stat.Key} = {stat.Value}");
            }
            DisplayHelper($"\n==== Buying Phase ====");
            DisplayHelper($"Store stock:");


            foreach (var piece in storeToPlay.GetStoreStock())
            {
                DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()} for {piece.GetPrice()} Gold");
            }

            UserInputPrompt();
            autoChessGame.SetGameStatus(GameStatusEnum.Completed);

        }

    }

    public static void DisplayHelper<T>(T value)
    {
        Console.WriteLine(value);
    }

    public static void CleanScreen()
    {
        Console.Clear();
    }

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
            DisplayHelper("Name cannot be set, please use another..");
            playerOneName = UserInputPrompt();
            checkOne = player.SetPlayerName(playerOneName);
        }

        CleanScreen();
        DisplayHelper("Name successfully set, press any key to continue..");
        UserInputPrompt();
    }

    public static List<Piece>? PieceInitializer()
    {
        var deserializer = new DataContractJsonSerializer(typeof(List<Piece>));
        FileStream fileStream= new FileStream(@"..\AutoChessSharp.PieceFactory\PiecesToPlay.json", FileMode.Open);

        List<Piece>? piecesToPlay = (List<Piece>?)deserializer.ReadObject(fileStream);

        return piecesToPlay;
    }

}
