using AutoChessSharp.Core;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

class PieceFactory
{
    public static List<Piece> SetAvailablePieces()
    {
        Piece axe = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Uncommon);
        Piece doom = new Piece(ArcheTypeEnum.Warrior, RarityEnum.Common);
        Piece huskar = new Piece(ArcheTypeEnum.Hunter, RarityEnum.Uncommon);
        Piece lina = new Piece(ArcheTypeEnum.Mage, RarityEnum.Common);
        Piece mortdred = new Piece(ArcheTypeEnum.Assassin, RarityEnum.Uncommon);
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

        return piecesToPlay;
    }


    static void Main()
    {
        DataContractJsonSerializerSettings Settings = new DataContractJsonSerializerSettings{
            UseSimpleDictionaryFormat = true,
        };

        List<Piece> piecesToSerialize = SetAvailablePieces();

        var jsonSer = new DataContractJsonSerializer(typeof(List<Piece>), Settings);
        FileStream streamer = new FileStream("PiecesToPlay.json", FileMode.Create);

        using (var writer = JsonReaderWriterFactory.CreateJsonWriter(streamer, Encoding.UTF8, true, true, " "))
        {
            jsonSer.WriteObject(writer, piecesToSerialize);
            streamer.Flush();

            Console.WriteLine($"successfully serialized {jsonSer}");
        }

    }
}