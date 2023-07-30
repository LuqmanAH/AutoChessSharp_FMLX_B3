using AutoChessSharp.Core;
namespace Program;

class Program
{
    static void Main()
    {
        // PlayerTest();
        // Console.WriteLine("\n----------\n");
        // BoardTest();
        // Console.WriteLine("\n----------\n");
        // PlayerInfoTest();
        // PlayerDictTest();
        // // GetHPTest();
        PieceAlgo2Test();

        Console.Read();
    }

    //* predefined atk and hp values based on the archetype used, no explicit method to set atk and hp, only name
    static void PieceAlgo2Test()
    {
        Board autoChessBoard = new Board(8);
        

        Piece warrior = new Piece(autoChessBoard);
        warrior.SetName("Valerian");

        Piece mage = new Piece(autoChessBoard, 2);
        mage.SetName("Celestio");

        Piece hunter = new Piece(autoChessBoard, 3);
        hunter.SetName("Wildtracker");

        Piece assassin = new Piece(autoChessBoard, 4);
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
}

class Helper
{
    public static void ProgramPrinter<T>(T value)
    {
        Console.WriteLine(value);
    }
}