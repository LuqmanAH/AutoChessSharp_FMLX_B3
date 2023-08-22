using AutoChessSharp.Core;

namespace Program;

partial class Program
{
    public static int BuyingPhaseLoop(GameRunner autoChessGame, Player player, List<AutoChessPiece> storeStock)
    {
        DisplayHelper("write the number of the piece in the store you want to buy, press other key to exit, you may only exit when you have at least 1 piece in your deck!");
        _ = int.TryParse(UserInputPrompt(), out int storeIndex);

        switch (storeIndex)
        {
            case 0:
                CleanScreen();
                _logger.Info($"{player.GetName()} exited store or attempted");
                break;

            case 1 :
                if (autoChessGame.BuyFromStore(player, storeStock[0]))
                {
                    CleanScreen();
                    DisplayHelper($"Successfully bought {storeStock[0].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerCurrentGold(player)}");
                    DisplayHelper($"\n{player.GetName()} Updated Piece List:");
 
                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper("\nPress any key to continue..");
                    _logger.Info($"Successfully bought {storeStock[0].GetName()}");
                    UserInputPrompt();
                    break;
                }
                CleanScreen();
                DisplayHelper("Not enough gold!");
                DisplayHelper("\nPress any key to continue..");
                _logger.Warn($"{player.GetID()} does not have enough gold");
                UserInputPrompt();
                break;

            case 2 :
                if (autoChessGame.BuyFromStore(player, storeStock[1]))
                {
                    CleanScreen();
                    DisplayHelper($"Successfully bought {storeStock[1].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerCurrentGold(player)}");
                    DisplayHelper($"\n{player.GetName()} Updated Piece List:");

                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper("\nPress any key to continue..");
                    _logger.Info($"Successfully bought {storeStock[1].GetName()}");
                    UserInputPrompt();
                    break;
                }
                CleanScreen();
                DisplayHelper("Not enough gold!");
                DisplayHelper("\nPress any key to continue..");
                _logger.Warn($"{player.GetID()} does not have enough gold");
                UserInputPrompt();
                break;

            case 3 :
                if (autoChessGame.BuyFromStore(player, storeStock[2]))
                {
                    CleanScreen();
                    DisplayHelper($"Successfully bought {storeStock[2].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerCurrentGold(player)}");
                    DisplayHelper($"\n{player.GetName()} Updated Piece List:");

                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper("\nPress any key to continue..");
                    _logger.Info($"Successfully bought {storeStock[2].GetName()}");
                    UserInputPrompt();
                    break;
                }
                CleanScreen();
                DisplayHelper("Not enough gold!");
                DisplayHelper("\nPress any key to continue..");
                _logger.Warn($"{player.GetID()} does not have enough gold");
                UserInputPrompt();
                break;

            case 4 :
                if (autoChessGame.BuyFromStore(player, storeStock[3]))
                {
                    CleanScreen();
                    DisplayHelper($"Successfully bought {storeStock[3].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerCurrentGold(player)}");
                    DisplayHelper($"\n{player.GetName()} Updated Piece List:");

                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper("\nPress any key to continue..");
                    _logger.Info($"Successfully bought {storeStock[3].GetName()}");
                    UserInputPrompt();
                    break;
                }
                CleanScreen();
                DisplayHelper("Not enough gold!");
                DisplayHelper("\nPress any key to continue..");
                _logger.Warn($"{player.GetID()} does not have enough gold");
                UserInputPrompt();
                break;

            case 5 :
                if (autoChessGame.BuyFromStore(player, storeStock[4]))
                {
                    CleanScreen();
                    DisplayHelper($"Successfully bought {storeStock[4].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerCurrentGold(player)}");
                    DisplayHelper($"\n{player.GetName()} Updated Piece List:");

                    DisplayPlayerPieces(autoChessGame, player);
                    DisplayHelper("\nPress any key to continue..");
                    _logger.Info($"Successfully bought {storeStock[4].GetName()}");
                    UserInputPrompt();
                    break;
                }
                CleanScreen();
                DisplayHelper("Not enough gold!");
                DisplayHelper("\nPress any key to continue..");
                _logger.Warn($"{player.GetID()} does not have enough gold");
                UserInputPrompt();
                break;
        
        }
        return storeIndex;
    }

    public static void DisplayPlayerPieces(GameRunner autoChessGame, Player player)
    {
        foreach (AutoChessPiece piece in autoChessGame.GetPlayerPiece(player).Cast<AutoChessPiece>())
        {
            DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
        }
    }

        private static void DisplayRegisteredPlayers(Dictionary<IPlayer, IPlayerInfo> playerInGame)
    {
        foreach (var playerData in playerInGame)
        {
            IPlayer player = playerData.Key;
            DisplayHelper($"{player.GetName()} is player {player.GetID()}");
        }
        DisplayHelper("Let The Game Begins.. When you're ready (Press any key)");
        _logger.Info("Game commencing");
    }

    private static async Task ClashPhase(GameRunner autoChessGame, Player[] players)
    {
        DisplayHelper($"\n{players[0].GetName()} Pieces:\n");
        DisplayPlayerPieces(autoChessGame, players[0]);
        DisplayHelper("\nVs\n");
        DisplayHelper($"\n{players[1].GetName()} Pieces:\n");
        DisplayPlayerPieces(autoChessGame, players[1]);

        DisplayHelper("\n\nInitiating Clash... press any key when ready");
        _logger.Info("Game clash commencing");
        UserInputPrompt();
        DisplayHelper($"Starting Randomized clash");

        for (int elapsedCountDown = 0; elapsedCountDown < autoChessGame.GetCountDown(); elapsedCountDown++)
        {
            await Task.Delay(1000);
            InlineDisplayHelper(".");
        }
    }

    private static void CheckFinishOrContinue(GameRunner autoChessGame)
    {
        if (autoChessGame.PlayersLeft() == 1)
        {
            IPlayer winner = autoChessGame.GetAlivePlayers().First();
            autoChessGame.SetGameStatus(GameStatusEnum.Completed);
            DisplayHelper("Game Concluded");
            DisplayHelper($"The winner is: {winner.GetName()}");
            _logger.Info($"Game ended in {autoChessGame.GetCurrentRound()} round: with {winner.GetName()} as the winner");
            UserInputPrompt();
        }

        else
        {

            DisplayHelper("Proceeding to next round...");
            autoChessGame.GoNextRound(1, 2);
            autoChessGame.GetStore().RerollStore();
            _logger.Info($"Proceeds to next round: {autoChessGame.GetCurrentRound()}");
            UserInputPrompt();
        }
    }

    private static GameRunner ExtractGamePieces(string pathForDebug, string pathForRelease, Board autoChessBoard)
    {
        GameRunner autoChessGame = new GameRunner(autoChessBoard);

        if (!autoChessGame.SetStorePieces(pathForDebug))
        {
            _logger.Warn("Data loaded from the release path");
            autoChessGame.SetStorePieces(pathForRelease);
        }
        else
        {
            _logger.Info("Data loaded from the debug path");
        }

        return autoChessGame;
    }
}
