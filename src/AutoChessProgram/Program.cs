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
        // Console.WriteLine("\n----------\n");
        PlayerDictTest();

        Console.Read();
    }

    static void PlayerTest()
    {
        //* Player generation case 1 test: null default ctor
        Player player_1 = new Player();
        Player player_2 = new Player("Lumine");
        Player player_3 = new Player();
        Helper.ProgramPrinter(player_1.GetName());

        //* Player generation case 2 test: ctor overload pass string as name
        Helper.ProgramPrinter(player_2.GetName()); 

        //* Set Player_1 name via method
        player_1.SetPlayerName("Lynette");
        Helper.ProgramPrinter(player_1.GetName());

        //* Player_2 allows name reset
        player_2.SetPlayerName("Jean Dancho");
        Helper.ProgramPrinter(player_2.GetName());

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
}

class Helper
{
    public static void ProgramPrinter(string value)
    {
        Console.WriteLine(value);
    }
}