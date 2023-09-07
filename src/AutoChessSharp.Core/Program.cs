using NLog;
using NLog.Config;
using AutoChessSharp.Core;
using NLog.LayoutRenderers.Wrappers;

namespace Program;

partial class Program
{
    private static Logger _logger = null!;
    private static void LoggerInitializer()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var nlogConfigPath = Path.Combine(currentDirectory, ".//Logs//Nlog.config");
        LogManager.Configuration = new XmlLoggingConfiguration(nlogConfigPath);

        _logger = LogManager.GetCurrentClassLogger();
    }

    async static Task Main()
    {
        //* env init
        LoggerInitializer();
        _logger.Info("Program Started");
        _logger.Info("Attempting to load database");
        string pathForDebug = @"Database\Pieces.ToPlay.json";
        string pathForRelease = @"..\..\..\Database\Pieces.ToPlay.json";
        Board autoChessBoard = new Board(8);
        GameRunner autoChessGame = ExtractGamePieces(pathForDebug, pathForRelease, autoChessBoard);

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

        autoChessGame.GetStore().RerollStore();

        //* player check
        CleanScreen();
        Dictionary<IPlayer, IPlayerInfo> playerInGame = autoChessGame.GetInGamePlayers();
        DisplayRegisteredPlayers(playerInGame);
        UserInputPrompt();

        //* Game commencing
        CleanScreen();
        autoChessGame.SetGameStatus(GameStatusEnum.Ongoing);

        while (autoChessGame.GetGameStatus() == GameStatusEnum.Ongoing)
        {
            List<AutoChessPiece> storeStock = autoChessGame.GetStore()
                                                            .GetStoreStock()
                                                            .Select(piece => (AutoChessPiece)piece)
                                                            .ToList();
            autoChessGame.SetCountDown(5);

            foreach (Player player in players)
            {
                DisplayHelper($"{player.GetName()} turn to pick\npress enter to continue..");
                _logger.Info($"Player {player.GetID()} turn to pick");
                UserInputPrompt();
                int buyOrLeave;
                bool placeOrLeave = true;
                do
                {
                    DisplayHelper("Pieces in deck: ");
                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper($"\n==== Buying Phase ====");
                    DisplayHelper($"index\tStore stock");
                    ShowStoreStock(storeStock);
                    buyOrLeave = BuyingPhaseLoop(autoChessGame, player,storeStock);
                    CleanScreen();
                }while (buyOrLeave != 0 || autoChessGame.GetPlayerPiece(player).Count == 0);

                while (placeOrLeave)
                {
                    bool promptValid;
                    bool postionLoopCondition;
                    bool pieceLoopCondition;
                    AutoChessPiece selectedPiece = default!;
                    Position placeDestination = new Position();

                    DisplayHelper("Select your owned piece index to place on the board, You must place at least one piece");
                    DisplayPlayerPieces(autoChessGame, player);

                    do
                    {
                        DisplayHelper("Enter your piece index: ");
                        int pieceIndex;
                        bool validIndex = int.TryParse(UserInputPrompt(), out pieceIndex);
                        pieceLoopCondition = validIndex && pieceIndex >= 0 && pieceIndex <=autoChessGame.GetPlayerPiece(player).Count(); 

                        if (pieceLoopCondition)
                        {
                            selectedPiece = (AutoChessPiece)autoChessGame.GetPlayerPiece(player)[pieceIndex - 1];
                            DisplayHelper($"Selected {selectedPiece.GetName()}. proceed to place position");
                            UserInputPrompt();
                        }

                        else
                        {
                            DisplayHelper("No character with that index!");
                            UserInputPrompt();
                        }

                    }while(!pieceLoopCondition);

                    do
                    {
                        DisplayHelper("Input your desired position (x,y): ");

                        var posInput = UserInputPrompt();
                        string[] coordInput = posInput!.Split(',');

                        bool condX = int.TryParse(coordInput[0], out int x);
                        bool condY = int.TryParse(coordInput[1], out int y);

                        postionLoopCondition = condX && condY;

                        if (postionLoopCondition)
                        {
                            placeDestination.SetX(x);
                            placeDestination.SetY(y);
                            DisplayHelper($"confirm position at: ({placeDestination.GetX()}, {placeDestination.GetY()})");
                            UserInputPrompt();

                            var validPlace = autoChessGame.PlacePiece(selectedPiece, placeDestination, player);
                            DisplayHelper($"Successfully placed {selectedPiece.GetName()} at ({selectedPiece.GetPosition().GetX()}, {selectedPiece.GetPosition().GetY()})");
                            UserInputPrompt();
                        }
                        else
                        {
                            DisplayHelper("Invalid Position format!");
                        }

                    }while (!postionLoopCondition);
    
                    do
                    {
                        DisplayHelper("Continue positioning? y/n");
                        var placeAgain = UserInputPrompt()!;
    
                        if (placeAgain!.ToLower() == "y")
                        {
                            placeOrLeave = true;
                            promptValid = false;
                        }
                        else if (placeAgain!.ToLower() == "n")
                        {
                            placeOrLeave = false;
                            promptValid = false;
                        }
                        else
                        {
                            promptValid = true;
                            DisplayHelper("unidentified character!");
                        }
                    } while (promptValid);
                }
                CleanScreen();
            }
        }

            //* Pre clash startup
            CleanScreen();
            await ClashPhase(autoChessGame, players);

            //* Chaos Ensues
            autoChessGame.GameClash();
            KeyValuePair<IPlayer, int> clashLoser = autoChessGame.GetClashLoser();
            KeyValuePair<IPlayer, int> clashWinner = autoChessGame.TryGetClashWinner();
            bool clashStatus = autoChessGame.TryDecreasePlayerHealth(clashLoser);

            //* Post-Chaos
            if (!clashStatus || clashWinner.Value < 0)
            {
                CleanScreen();
                autoChessGame.SetCountDown(0);
                DisplayHelper("Clash Returned as tied, no damage done to players...");
                _logger.Warn("Clash tied, no winner or loser");
            }
            else
            {
                CleanScreen();
                autoChessGame.SetCountDown(0);
                DisplayHelper($"{clashWinner.Key.GetName()} Wins the clash with {clashWinner.Value} Pieces left!");
                DisplayHelper($"{clashLoser.Key.GetName()} has lost the clash, damaged, and the current HP is now: {autoChessGame.ShowPlayerHealth(clashLoser.Key)}");
                _logger.Info($"clash succeed with {clashWinner.Key.GetName()} as the winner and {autoChessGame.GetClashLoser().Key.GetName()} as the loser");
            }

            DisplayHelper($"\n{players[0].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[0]);
            DisplayHelper($"\n{players[1].GetName()} Pieces:\n");
            DisplayPlayerPieces(autoChessGame, players[1]);

            DisplayHelper("\n\nPress any key..");
            _logger.Info("Game clash completed");
            UserInputPrompt();

            CleanScreen();
            DisplayHelper($"=== Round {autoChessGame.GetCurrentRound()} Concluded ===");
            foreach (var playerHealth in autoChessGame.ShowPlayerHealth())
            {
                DisplayHelper($"{playerHealth.Key.GetName()} Now Has HP of {playerHealth.Value}");
                _logger.Info($"{playerHealth.Key.GetName()} updated HP: {playerHealth.Value}");
            }

            UserInputPrompt();

            //? might be better to implement a standalone logic in gamerunner to invoke winner
            CleanScreen();
            CheckFinishOrContinue(autoChessGame);
        }

        static void NewMethod(GameRunner autoChessGame, List<AutoChessPiece> storeStock, Player player)
        {
            DisplayHelper($"{player.GetName()} turn to pick\npress enter to continue..");
            _logger.Info($"Player {player.GetID()} turn to pick");
            UserInputPrompt();
            int buyOrLeave;
            do
            {
                CleanScreen();
                DisplayHelper($"==== Beginning Round {autoChessGame.GetCurrentRound()} ====\n");
                DisplayHelper($"player {player.GetName()} stats:");
                ShowPlayerStats(autoChessGame, player);
                DisplayHelper($"\nplayer {player.GetName()} pieces List:");
                DisplayPlayerPieces(autoChessGame, player);
                DisplayHelper($"\n==== Buying Phase ====");
                DisplayHelper($"index\tStore stock");
                ShowStoreStock(storeStock);
                buyOrLeave = BuyingPhaseLoop(autoChessGame, player, storeStock);

            } while (buyOrLeave != 0 || autoChessGame.GetPlayerPiece(player).Count == 0);
        }
    }