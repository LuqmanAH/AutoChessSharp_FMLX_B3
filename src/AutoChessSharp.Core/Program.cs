using AutoChessSharp.Core;

class Program
{
    public static void Main()
    {
        List<Piece> piecesToPlay = SetAvailablePieces();
        Store storeToPlay = new Store(piecesToPlay);
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

        Dictionary<IPlayer, PlayerInfo> playerInGame = autoChessGame.GetAllPlayers();

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

            DisplayHelper($"==== Beginning Round {autoChessGame.GetCurrentRound()} ====");
            DisplayHelper($"==== Buying Phase ====");
            DisplayHelper($"Store stock:");

            foreach (var piece in storeToPlay.GetStoreStock())
            {
                DisplayHelper($"{piece.GetName()} as {piece.GetArcheType()} for {piece.GetPrice()}");
            }

            UserInputPrompt();
            autoChessGame.SetGameStatus(GameStatusEnum.Completed);
        }

    }

    public static List<Piece> SetAvailablePieces()
    {
        Piece axe = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon);
        Piece doom = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Common);
        Piece huskar = new Piece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon);
        Piece lina = new Piece(ArcheTypeEnum.Mage, RarityEnum.Common);
        Piece mortdred = new Piece(ArcheTypeEnum.Assassin, RarityEnum.Uncommon);
        Piece ezalor = new Piece(ArcheTypeEnum.Mage, RarityEnum.Uncommon);

        axe.SetName("Axe");
        doom.SetName("Doom");
        huskar.SetName("Huskar");
        lina.SetName("Lina");
        mortdred.SetName("Mortdred");
        ezalor.SetName("Ezalor");

        List<Piece> piecesToPlay = new List<Piece>()
        {
            axe,
            doom,
            huskar,
            lina,
            mortdred,
            ezalor
        };

        return piecesToPlay;
    }

    public static void DisplayHelper<T>(T value)
    {
        Console.WriteLine(value);
    }

    public static void CleanScreen()
    {
        Console.Clear();
    }

    public static string UserInputPrompt()
    {
        var userInput = Console.ReadLine().ToString();

        return userInput;
    }

    public static void InitPrompt(GameRunner gameRunner, Player player, int ID)
    {
        CleanScreen();
        DisplayHelper("====Auto Chess Game====");
        DisplayHelper("* This is the two players version of the game ");
        DisplayHelper($"* Enter string as player {ID} name: ");
        string playerOneName = UserInputPrompt();
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
}
