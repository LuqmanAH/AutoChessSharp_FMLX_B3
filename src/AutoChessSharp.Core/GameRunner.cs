namespace AutoChessSharp.Core;
public class GameRunner
{
    private Dictionary<IPlayer, PlayerInfo> playerDetail;

    public GameRunner()
    {
        playerDetail = new Dictionary<IPlayer, PlayerInfo>();
    }

    public bool AddPlayer(IPlayer player)
    {
        if (playerDetail.ContainsKey(player))
        {
            return false;
        }
        playerDetail.Add(player, new PlayerInfo());
        return true;
    }

    public Dictionary<IPlayer, PlayerInfo> GetAllPlayers()
    {
        return playerDetail;
    }
}
