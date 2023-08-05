using AutoChessSharp.Core;
using System.Runtime.Serialization.Json;

partial class Program
{
    public static void Main()
    {
        //* env init
        List<Piece>? piecesToPlay = PieceInitializer();
        Store? storeToPlay = new Store(piecesToPlay);
        storeToPlay.RerollStore();

        Board autoChessBoard = new Board(8);
        GameRunner autoChessGame = new GameRunner(autoChessBoard, storeToPlay);
        
        //* players insertion
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

        Dictionary<IPlayer, PlayerInfo> playerInGame = autoChessGame.GetInGamePlayers();

        //* player check
        CleanScreen();
        foreach (var playerData in playerInGame)
        {
            IPlayer player = playerData.Key;
            DisplayHelper($"{player.GetName()} is player {player.GetID()}");
        }
        DisplayHelper("Let The Game Begins.. When you're ready (Press any key)");
        UserInputPrompt();

        //* Game commencing
        CleanScreen();
        autoChessGame.SetGameStatus(GameStatusEnum.Ongoing);
        Player player1 = autoChessGame.GetPlayer(1);
        Player player2 = autoChessGame.GetPlayer(2);

        while (autoChessGame.GetGameStatus() == GameStatusEnum.Ongoing)
        {
            List<Piece> storeStock = storeToPlay.GetStoreStock();

            DisplayHelper($"==== Beginning Round {autoChessGame.GetCurrentRound()} ====\n");

            foreach (Player player in players)
            {
                int buyOrLeave = -1;
                do
                {
                    CleanScreen();
                    DisplayHelper($"player {player.GetName()} stats:");
                    ShowPlayerStats(autoChessGame, player);
                    DisplayHelper($"\n==== Buying Phase ====");
                    DisplayHelper($"Store stock:");
                    ShowStoreStock(storeStock);
                    buyOrLeave = BuyingPhaseLoop(autoChessGame, player,storeStock);

                }while (buyOrLeave != 0);

            }

            UserInputPrompt();
            autoChessGame.SetGameStatus(GameStatusEnum.Completed);

        }

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
            DisplayHelper("Name cannot be set, please Input another: ");
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
