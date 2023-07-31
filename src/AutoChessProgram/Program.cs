/*

* as a result of the autochess.core being a library class conditional compilation is mandatory for now
! TEST for testing, PROGRAM for the real game

*/

#define TEST
using AutoChessSharp.Core;
namespace Program;

public class Program
{
    static void Main()
    {
        #if TEST
        // PlayerTest();
        // BoardTest();
        // PlayerInfoTest();
        // PlayerDictTest();
        // PieceAlgo2Test();
        StoreCreationTest();
        Console.Read();

        #elif PROGRAM
        Helper.ProgramPrinter("ready to implement");

        #else
        #error Production Not Allowed
        #endif
    }

    //* predefined atk and hp values based on the archetype used, no explicit method to set atk and hp, only name
    static void PieceAlgo2Test()
    {
        Board autoChessBoard = new Board(8);
        

        Piece warrior = new Piece(1, 1);
        warrior.SetName("Valerian");

        Piece mage = new Piece(2, 1);
        mage.SetName("Celestio");

        Piece hunter = new Piece(3, 1);
        hunter.SetName("Wildtracker");

        Piece assassin = new Piece(4, 1);
        assassin.SetName("Veliona");

        Helper.ProgramPrinter(warrior.GetName());
        Helper.ProgramPrinter("HP: " + warrior.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + warrior.GetAttack());

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(mage.GetName());
        Helper.ProgramPrinter("HP: " + mage.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + mage.GetAttack());

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(hunter.GetName());
        Helper.ProgramPrinter("HP: " + hunter.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + hunter.GetAttack());

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(assassin.GetName());
        Helper.ProgramPrinter("HP: " + assassin.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + assassin.GetAttack());
    }

    static void PlayerTest()
    {
        //* Player generation case 1 test: null default ctor
        Player player_1 = new Player();
        Player player_2 = new Player("Lumine");
        Player player_3 = new Player("Pestering Fauna");
        Helper.ProgramPrinter(player_1.GetName());

        //* Player generation case 2 test: ctor overload pass string as name
        Helper.ProgramPrinter(player_2.GetName()); 

        //* Set Player_1 name via method
        player_1.SetPlayerName("Lynette");
        Helper.ProgramPrinter(player_1.GetName());

        //* Player_2 allows name reset
        player_2.SetPlayerName("Jean Dancho");
        Helper.ProgramPrinter(player_2.GetName());

        //* Setter test on overwriting identic playerName
        Helper.ProgramPrinter(player_3.GetName());
        bool setInfo = player_3.SetPlayerName("Pestering Fauna");
        Helper.ProgramPrinter(player_3.GetName());
        Helper.ProgramPrinter($"Operation resulted in: {setInfo}");

        Helper.ProgramPrinter(player_1.GetID().ToString());
        Helper.ProgramPrinter(player_2.GetID().ToString());
        Helper.ProgramPrinter(player_3.GetID().ToString());
    }

    static void BoardTest()
    {
        Board autoChessBoard = new Board();
        Helper.ProgramPrinter(autoChessBoard.GetBoardSize().ToString());

        autoChessBoard.SetBoardSize(8);
        Helper.ProgramPrinter(autoChessBoard.GetBoardSize().ToString());
    }

    static void PlayerInfoTest()
    {
        PlayerInfo p1_info = new PlayerInfo();
        Helper.ProgramPrinter("Level: " + p1_info.GetLevel().ToString());
        Helper.ProgramPrinter("Gold: " + p1_info.GetGold().ToString());
        Helper.ProgramPrinter("Exp: " + p1_info.GetExperience().ToString());
        Helper.ProgramPrinter("HP: " + p1_info.GetHealth().ToString());
    }

    static void PlayerDictTest()
    {
        GameRunner autoChess = new GameRunner();
        Player player1= new Player("Baal");
        Player player2= new Player("Buer");

        autoChess.AddPlayer(player1);
        autoChess.AddPlayer(player2);

        Dictionary<IPlayer, PlayerInfo> ListOfPlayers = autoChess.GetAllPlayers();

        foreach (var playersInfo in ListOfPlayers)
        {
            IPlayer player = playersInfo.Key;
            PlayerInfo info = playersInfo.Value;
            Helper.ProgramPrinter($"{player.GetName()} associated with {info}");

        }
    }

    static void GetHPTest()
    {
        GameRunner autoChess = new GameRunner();
        Player player1= new Player("Baal");
        Player player2= new Player("Buer");
        Player player3= new Player("Barbatos");

        autoChess.AddPlayer(player1);
        autoChess.AddPlayer(player2);

        Dictionary<IPlayer, int> playerHealthData = autoChess.ShowPlayerHealth();

        foreach (var kvp in playerHealthData)
        {
            Helper.ProgramPrinter($"Player {kvp.Key.GetName()} has HP of {kvp.Value}");
        }

        int player1Health = autoChess.ShowPlayerHealth(player1);
        Helper.ProgramPrinter($"{player1.GetName()} currently has HP of {player1Health}");

        try
        {
            int player3Health = autoChess.ShowPlayerHealth(player3);
        }
        catch(KeyNotFoundException ex)
        {
            Console.WriteLine($"{player3.GetName()} {ex.Message}");
        }

    }

    static void StoreCreationTest()
    {
        Piece axe = new Piece(1, 2);
        axe.SetName("Axe");

        Piece doom = new Piece(1, 1);
        doom.SetName("Doom");

        Piece huskar = new Piece(3, 2);
        huskar.SetName("Huskar");

        Piece lina = new Piece(2, 1);
        lina.SetName("Lina");

        Piece mortdred = new Piece(4, 2);
        mortdred.SetName("Mortdred");

        Piece ezalor = new Piece(2, 2);
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

        Store store= new Store(piecesToPlay);
        store.RerollStore();
        List<Piece> storePieces = store.GetPrice();

        foreach (var piece in storePieces)
        {
            string? pieceName = piece.GetName();
            int piecePrice = piece.GetPrice();

            Helper.ProgramPrinter($"{pieceName} is available for {piecePrice} Golds");
        }

        try
        {
            Helper.ProgramPrinter(store.GetPrice(lina));
        }
        catch (Exception ex)
        {
            Helper.ProgramPrinter(ex.Message);
        }
        
    }

}

class Helper
{
    public static void ProgramPrinter<T>(T value)
    {
        Console.WriteLine(value);
    }
}