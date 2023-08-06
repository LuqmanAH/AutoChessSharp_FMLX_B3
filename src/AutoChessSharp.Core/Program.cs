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

        while (autoChessGame.GetGameStatus() == GameStatusEnum.Ongoing)
        {
            List<Piece> storeStock = autoChessGame.GetStore().GetStoreStock();
            autoChessGame.SetCountDown(5);

            foreach (Player player in players)
            {
                int buyOrLeave;
                do
                {
                    CleanScreen();
                    DisplayHelper($"==== Beginning Round {autoChessGame.GetCurrentRound()} ====\n");
                    DisplayHelper($"player {player.GetName()} stats:");
                    ShowPlayerStats(autoChessGame, player);
                    DisplayHelper($"\n player {player.GetName()} pieces List:");
                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper($"\n==== Buying Phase ====");
                    DisplayHelper($"Store stock:");
                    ShowStoreStock(storeStock);
                    buyOrLeave = BuyingPhaseLoop(autoChessGame, player,storeStock);

                }while (buyOrLeave != 0);

            }

            //TODO decouple and try to omit using thread sleep
            CleanScreen();

            DisplayHelper($"\n{players[0].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[0]);
            DisplayHelper("\nVs\n");
            DisplayHelper($"\n{players[1].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[1]);
            
            DisplayHelper("\n\nInitiating Clash... press any key when ready");
            UserInputPrompt();
            DisplayHelper($"Starting Randomized clash");
            for (int elapsedCountDown = 0; elapsedCountDown < autoChessGame.GetCountDown(); elapsedCountDown++)
            {
                Thread.Sleep(1000);
                InlineDisplayHelper(".");
            }

            SortedDictionary<int, IPlayer> afterClash = autoChessGame.GameClash();
            KeyValuePair<int, IPlayer> clashLoser = autoChessGame.GetClashLoser(afterClash);
            KeyValuePair<int, IPlayer> clashWinner = autoChessGame.GetClashWinner(afterClash);
            autoChessGame.DecreasePlayerHealth(clashLoser);

            CleanScreen();
            autoChessGame.SetCountDown(0);
            DisplayHelper($"{clashWinner.Value.GetName()} Wins the clash with {clashWinner.Key} Pieces left!");
            DisplayHelper($"{clashLoser.Value.GetName()} has lost the clash, damaged, and the current HP is now: {autoChessGame.ShowPlayerHealth(clashLoser.Value)}");

            DisplayHelper($"\n{players[0].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[0]);
            DisplayHelper($"\n{players[1].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[1]);

            DisplayHelper("\n\nPress any key..");
            UserInputPrompt();

            CleanScreen();
            DisplayHelper($"=== Round {autoChessGame.GetCurrentRound()} Concluded ===");
            foreach (var playerHealth in autoChessGame.ShowPlayerHealth())
            {
                DisplayHelper($"{playerHealth.Key.GetName()} Now Has HP of {playerHealth.Value}");
            }

            UserInputPrompt();

            CleanScreen();
            if (autoChessGame.PlayersLeft() == 1)
            {
                IPlayer winner  = autoChessGame.GetAlivePlayers().First();
                autoChessGame.SetGameStatus(GameStatusEnum.Completed);
                DisplayHelper("Game Concluded");
                DisplayHelper($"The winner is: {winner.GetName()}");
                UserInputPrompt();
            }

            else
            {

                DisplayHelper("Proceeding to next round...");
                autoChessGame.GoNextRound();
                storeToPlay.RerollStore();
                UserInputPrompt();
            }
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
        FileStream fileStream= new FileStream(@"..\..\..\..\AutoChessSharp.PieceFactory\PiecesToPlay.json", FileMode.Open);

        List<Piece>? piecesToPlay = (List<Piece>?)deserializer.ReadObject(fileStream);

        return piecesToPlay;
    }

}
