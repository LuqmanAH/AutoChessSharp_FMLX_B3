using AutoChessSharp.Core;

partial class Program
{
    public static int BuyingPhaseLoop(GameRunner autoChessGame, Player player1, List<Piece> storeStock)
    {
        DisplayHelper("write the number of the piece in the store you want to buy, write 0 to exit store..");
        int storeIndex = int.Parse(UserInputPrompt());

        switch (storeIndex)
        {
            case 0:
                DisplayHelper("Exitting store..");
                break;

            case 1 :
                if (autoChessGame.BuyFromStore(player1, storeStock[0]))
                {
                    DisplayHelper($"Successfully bought {storeStock[0].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerStats(player1)["Gold"]}");
                    DisplayHelper($"{player1.GetName()} Updated Piece List:");

                    foreach (Piece piece in autoChessGame.GetPlayerPiece(player1))
                    {
                        DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
                    }
                    DisplayHelper("Press any key to continue..");
                    UserInputPrompt();
                    break;
                }
                DisplayHelper("Not enough gold!");
                DisplayHelper("Press any key to continue..");
                UserInputPrompt();
                break;

            case 2 :
                if (autoChessGame.BuyFromStore(player1, storeStock[1]))
                {
                    DisplayHelper($"Successfully bought {storeStock[1].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerStats(player1)["Gold"]}");
                    DisplayHelper($"{player1.GetName()} Updated Piece List:");

                    foreach (Piece piece in autoChessGame.GetPlayerPiece(player1))
                    {
                        DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
                    }
                    DisplayHelper("Press any key to continue..");
                    UserInputPrompt();
                    break;
                }
                DisplayHelper("Not enough gold!");
                DisplayHelper("Press any key to continue..");
                UserInputPrompt();
                break;

            case 3 :
                if (autoChessGame.BuyFromStore(player1, storeStock[2]))
                {
                    DisplayHelper($"Successfully bought {storeStock[2].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerStats(player1)["Gold"]}");
                    DisplayHelper($"{player1.GetName()} Updated Piece List:");

                    foreach (Piece piece in autoChessGame.GetPlayerPiece(player1))
                    {
                        DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
                    }
                    DisplayHelper("Press any key to continue..");
                    UserInputPrompt();
                    break;
                }
                DisplayHelper("Not enough gold!");
                DisplayHelper("Press any key to continue..");
                UserInputPrompt();
                break;

            case 4 :
                if (autoChessGame.BuyFromStore(player1, storeStock[3]))
                {
                    DisplayHelper($"Successfully bought {storeStock[3].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerStats(player1)["Gold"]}");
                    DisplayHelper($"{player1.GetName()} Updated Piece List:");

                    foreach (Piece piece in autoChessGame.GetPlayerPiece(player1))
                    {
                        DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
                    }
                    DisplayHelper("Press any key to continue..");
                    UserInputPrompt();
                    break;
                }
                DisplayHelper("Not enough gold!");
                DisplayHelper("Press any key to continue..");
                UserInputPrompt();
                break;

            case 5 :
                if (autoChessGame.BuyFromStore(player1, storeStock[4]))
                {
                    DisplayHelper($"Successfully bought {storeStock[4].GetName()}");
                    DisplayHelper($"Updated Gold: {autoChessGame.GetPlayerStats(player1)["Gold"]}");
                    DisplayHelper($"{player1.GetName()} Updated Piece List:");

                    foreach (Piece piece in autoChessGame.GetPlayerPiece(player1))
                    {
                        DisplayHelper($"{piece.GetName()} {piece.GetRarityEnum()} {piece.GetArcheType()}");
                    }
                    DisplayHelper("Press any key to continue..");
                    UserInputPrompt();
                    break;
                }
                DisplayHelper("Not enough gold!");
                DisplayHelper("Press any key to continue..");
                UserInputPrompt();
                break;
        
        }
        return storeIndex;
    }
}
