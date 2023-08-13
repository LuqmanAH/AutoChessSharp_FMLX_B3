using AutoChessSharp.Core;
using System.Text;
using System.Runtime.Serialization.Json;

class PieceFactory
{
    public static List<AutoChessPiece> SetAvailablePieces()
    {
        AutoChessPiece axe = new AutoChessPiece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon);
        AutoChessPiece doom = new AutoChessPiece(ArcheTypeEnum.Warrior, RarityEnum.Common);
        AutoChessPiece huskar = new AutoChessPiece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon);
        AutoChessPiece lina = new AutoChessPiece(ArcheTypeEnum.Mage, RarityEnum.Common);
        AutoChessPiece mortdred = new AutoChessPiece(ArcheTypeEnum.Assassin, RarityEnum.Uncommon);
        AutoChessPiece ezalor = new AutoChessPiece(ArcheTypeEnum.Mage, RarityEnum.Uncommon);

        axe.SetName("Axe");
        doom.SetName("Doom");
        huskar.SetName("Huskar");
        lina.SetName("Lina");
        mortdred.SetName("Mortdred");
        ezalor.SetName("Ezalor");

        List<AutoChessPiece> piecesToPlay = new List<AutoChessPiece>()
        {
            axe,
            doom,
            huskar,
            lina,
            mortdred,
            ezalor
        };

        return piecesToPlay;
    }


    static void Main()
    {
        DataContractJsonSerializerSettings Settings = new DataContractJsonSerializerSettings{
            UseSimpleDictionaryFormat = true,
        };

        List<AutoChessPiece> piecesToSerialize = SetAvailablePieces();

        string path = @"..\AutoChessSharp.Core\Database\Pieces.ToPlay.json";

        var jsonSer = new DataContractJsonSerializer(typeof(List<AutoChessPiece>), Settings);
        FileStream streamer = new FileStream(path, FileMode.Create);

        using (var writer = JsonReaderWriterFactory.CreateJsonWriter(streamer, Encoding.UTF8, true, true, " "))
        {
            jsonSer.WriteObject(writer, piecesToSerialize);
            streamer.Flush();

            Console.WriteLine($"successfully serialized {jsonSer}");
        }

    }
}