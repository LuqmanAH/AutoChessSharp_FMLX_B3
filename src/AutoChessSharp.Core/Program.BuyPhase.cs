using AutoChessSharp.Core;

namespace Program;

partial class Program
{
    public static int BuyingPhaseLoop(GameRunner autoChessGame, Player player, List<Piece> storeStock)
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
        foreach (Piece piece in autoChessGame.GetPlayerPiece(player))
        {
            DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
        }
    }
}
