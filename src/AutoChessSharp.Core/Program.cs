using AutoChessSharp.Core;

class Program
{
    public static void Main(string[] args)
    {
        List<Piece> piecesToPlay = SetAvailablePieces();
        Store storeToPlay = new Store(piecesToPlay);
        storeToPlay.RerollStore();

        Board autoChessBoard = new Board(8);
        GameRunner autoChessGame = new GameRunner(autoChessBoard, storeToPlay);
        
        Clean();
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

        Clean();
        foreach (var playerData in playerInGame)
        {
            IPlayer player = playerData.Key;
            DisplayHelper($"{player.GetName()} is in game");
        }
        DisplayHelper("Let The Game Begins.. When you're ready (Press any key)");
        UserInputPrompt();
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

    public static void Clean()
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
        Clean();
        DisplayHelper("====Auto Chess Game====");
        DisplayHelper("* This is the two players version of the game ");
        DisplayHelper($"* Enter string as player {ID} name: ");
        string playerOneName = UserInputPrompt();
        bool checkOne = player.SetPlayerName(playerOneName);
        gameRunner.AddPlayer(player);

        while (!checkOne)
        {
            Clean();
            DisplayHelper("Name cannot be set, please use another..");
            playerOneName = UserInputPrompt();
            checkOne = player.SetPlayerName(playerOneName);
        }

        Clean();
        DisplayHelper("Name successfully set, press any key to continue..");
        UserInputPrompt();
    }
}
