using AutoChessSharp.Core;

partial class Program
{
    async static Task Main()
    {
        //* env init
        string pathForDebug = @"Database\Pieces.ToPlay.json";
        string pathForRelease = @"..\..\..\Database\Pieces.ToPlay.json";
        Board autoChessBoard = new Board(8);

        GameRunner autoChessGame = new GameRunner(autoChessBoard);

        if (!autoChessGame.SetStorePieces(pathForDebug))
        {
            autoChessGame.SetStorePieces(pathForRelease);
        }
        
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
        autoChessGame.GetStore().RerollStore();

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

                }while (buyOrLeave != 0 || autoChessGame.GetPlayerPiece(player).Count == 0);

            }

            //* Pre clash startup
            //TODO decouple and try to omit using thread sleep (resolved, yet to decouple)
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
                await Task.Delay(1000);
                InlineDisplayHelper(".");
            }

            //* Chaos Ensues
            //TODO Debug step in to this brok
            autoChessGame.GameClash();
            KeyValuePair<int, IPlayer> clashLoser = autoChessGame.GetClashLoser();
            KeyValuePair<int, IPlayer> clashWinner = autoChessGame.GetClashWinner();
            autoChessGame.DecreasePlayerHealth(clashLoser);

            //* Post-Chaos
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

            //? might be better to implement a standalone logic in gamerunner to invoke winner
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
                autoChessGame.GoNextRound(1, 2);
                autoChessGame.GetStore().RerollStore();
                UserInputPrompt();
            }
        }

    }

}
