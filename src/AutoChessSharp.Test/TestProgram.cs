using AutoChessSharp.Core;
namespace Program;

public class Program
{
    static void Main()
    {
        // PlayerTest();
        // BoardTest();
        // PlayerInfoTest();
        // PlayerDictTest();
        // PieceAlgo2Test();
        // StoreCreationTest();
        GameClashTest();
        Console.Read();

    }

    //* predefined atk and hp values based on the archetype used, no explicit method to set atk and hp, only name
    static void PieceAlgo2Test()
    {
        Piece warrior = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon);
        warrior.SetName("Valerian");

        Piece mage = new Piece(ArcheTypeEnum.Mage, RarityEnum.Common);
        mage.SetName("Celestio");

        Piece hunter = new Piece(ArcheTypeEnum.Hunter, RarityEnum.Rare);
        hunter.SetName("Wildtracker");

        Piece assassin = new Piece(ArcheTypeEnum.Assassin, RarityEnum.Legendary);
        assassin.SetName("Veliona");

        Helper.ProgramPrinter(warrior.GetName());
        Helper.ProgramPrinter("HP: " + warrior.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + warrior.GetAttack());
        Helper.ProgramPrinter($"Default Position:({warrior.GetPosition().GetX()}, {warrior.GetPosition().GetY()})");

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(mage.GetName());
        Helper.ProgramPrinter("HP: " + mage.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + mage.GetAttack());
        Helper.ProgramPrinter($"Default Position:({mage.GetPosition().GetX()}, {mage.GetPosition().GetY()})");

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(hunter.GetName());
        Helper.ProgramPrinter("HP: " + hunter.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + hunter.GetAttack());
        Helper.ProgramPrinter($"Default Position:({hunter.GetPosition().GetX()}, {hunter.GetPosition().GetY()})");

        Console.WriteLine("\n----------\n");
        Helper.ProgramPrinter(assassin.GetName());
        Helper.ProgramPrinter("HP: " + assassin.GetHealthPoint());
        Helper.ProgramPrinter("ATK: " + assassin.GetAttack());
        Helper.ProgramPrinter($"Default Position:({assassin.GetPosition().GetX()}, {assassin.GetPosition().GetY()})");
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
        List<Piece> pieces_P1 = new List<Piece>()
        {
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon,"budi"),
            new Piece(ArcheTypeEnum.Mage, RarityEnum.Rare,"poco"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Rare,"justin"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon,"akba"),
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Rare,"sule"),
        };

        Helper.ProgramPrinter("Level: " + p1_info.GetLevel().ToString());
        Helper.ProgramPrinter("Gold: " + p1_info.GetGold().ToString());
        Helper.ProgramPrinter("Exp: " + p1_info.GetExperience().ToString());
        Helper.ProgramPrinter("HP: " + p1_info.GetHealth().ToString());

        bool stat = p1_info.SetPieces(pieces_P1);

        if (stat == true)
        {
            Helper.ProgramPrinter("set piece success");
            List<Piece>? p1_Pieces = p1_info.GetPieces();
            
            if (p1_Pieces != null)
            {
                foreach (var piece in p1_Pieces)
                {
                    Helper.ProgramPrinter($"{piece.GetName()} a brave {piece.GetArcheType()} belongs to p1");
                }

            }

        }
        else
        {
            Helper.ProgramPrinter("set piece FAIL!");
        }
    }

    static void PlayerDictTest()
    {
        List<Piece> pieces = new List<Piece>();
        Board board = new(8);
        Store store = new(pieces);

        GameRunner autoChess = new GameRunner(board);
        Player player1= new Player("Baal");
        Player player2= new Player("Buer");

        autoChess.AddPlayer(player1);
        autoChess.AddPlayer(player2);

        Dictionary<IPlayer, PlayerInfo> ListOfPlayers = autoChess.GetInGamePlayers();

        foreach (var playersInfo in ListOfPlayers)
        {
            IPlayer player = playersInfo.Key;
            PlayerInfo info = playersInfo.Value;
            Helper.ProgramPrinter($"{player.GetName()} associated with {info}");

        }

        int playersLeft = autoChess.PlayersLeft();
        Helper.ProgramPrinter(playersLeft);
    }

    static void GetHPTest()
    {
        List<Piece> pieces = new List<Piece>();
        Board board = new(8);
        Store store = new(pieces);

        GameRunner autoChess = new GameRunner(board);
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
        Piece axe = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon);
        Piece doom = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Common);
        Piece huskar = new Piece(ArcheTypeEnum.Hunter, RarityEnum.Rare);
        Piece lina = new Piece(ArcheTypeEnum.Mage, RarityEnum.Epic);
        Piece mortdred = new Piece(ArcheTypeEnum.Assassin,RarityEnum.Common);
        Piece ezalor = new Piece(ArcheTypeEnum.Mage, RarityEnum.Uncommon);

        axe.SetName("Axe");
        doom.SetName("Doom");
        huskar.SetName("Huskar");
        lina.SetName("Lina");
        mortdred.SetName("Mortdred");
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
        List<Piece> storePieces = store.GetStoreStock();

        foreach (var piece in storePieces)
        {
            string? pieceName = piece.GetName();
            int piecePrice = piece.GetPrice();

            Helper.ProgramPrinter($"{pieceName} is available for {piecePrice} Golds");
        }

        try
        {
            Helper.ProgramPrinter(store.GetPiecePrice(lina));
        }
        catch (Exception ex)
        {
            Helper.ProgramPrinter(ex.Message);
        }

        // store.LockStore();

        try
        {
            store.RerollStore();
        }
        catch (Exception ex)
        {
            Helper.ProgramPrinter(ex.Message);
        }
        
    }

    static void GameClashTest()
    {
        List<Piece> pieces = new List<Piece>();

        List<Piece> pieces_P1 = new List<Piece>()
        {
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon,"budi"),
            new Piece(ArcheTypeEnum.Mage, RarityEnum.Rare,"poco"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Rare,"justin"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon,"akba"),
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Rare,"sule"),
        };

        List<Piece> pieces_P2 = new List<Piece>()
        {
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon,"budi"),
            new Piece(ArcheTypeEnum.Mage, RarityEnum.Rare,"poco"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Rare,"justin"),
            new Piece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon,"akba"),
            new Piece(ArcheTypeEnum.Warrior, RarityEnum.Rare,"sule"),
        };

        Board board = new(8);
        Store store = new(pieces);

        GameRunner autoChess = new GameRunner(board);
        Player player1= new Player("Baal");
        Player player2= new Player("Buer");

        autoChess.AddPlayer(player1);
        autoChess.AddPlayer(player2);


        Helper.ProgramPrinter(autoChess.PlayersLeft());

        foreach (Piece piece in pieces_P1)
        {
            Helper.ProgramPrinter(autoChess.BuyFromStore(player1, piece));
        }

        foreach (Piece piece in pieces_P2)
        {
            Helper.ProgramPrinter(autoChess.BuyFromStore(player2, piece));
        }

        Helper.ProgramPrinter(" ");
        List<Piece> baalPieces = autoChess.GetPlayerPiece(player1);
        List<Piece> buerPieces = autoChess.GetPlayerPiece(player2);

        SortedDictionary<int, IPlayer> afterClash =  autoChess.GameClash();
        KeyValuePair<int, IPlayer> clashLoser = autoChess.GetClashLoser(afterClash);

        int loserUpdatedHealth = autoChess.DecreasePlayerHealth(clashLoser);
        Helper.ProgramPrinter($"{clashLoser.Value.GetName()} has lost the clash, damaged, and the current HP is now: {loserUpdatedHealth}");
    }
}

class Helper
{
    public static void ProgramPrinter<T>(T value)
    {
        Console.WriteLine(value);
    }
}