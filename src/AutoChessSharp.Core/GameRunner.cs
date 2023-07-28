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
        bool addSuccess = playerDetail.TryAdd(player, new PlayerInfo());
        return addSuccess;
    }

    public Dictionary<IPlayer, PlayerInfo> GetAllPlayers()
    {
        return playerDetail;
    }

    public Dictionary<IPlayer, int> ShowPlayerHealth()
    {
        if (playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Players Yet!");
        }

        Dictionary<IPlayer, int> PlayersHealth = new();
        foreach (var details in playerDetail)
        {
            IPlayer playerInGame = details.Key;
            int playerHealthPoint = details.Value.GetHealth();

            PlayersHealth.Add(playerInGame, playerHealthPoint);
        }
        return PlayersHealth;
    }
    public int ShowPlayerHealth(IPlayer player)
    {
        if (playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Players Yet!");
        }

        if (!playerDetail.ContainsKey(player))
        {
            throw new KeyNotFoundException(message : "Player Not Found");
        }

        int playerHealth = 0;
        foreach (var details in playerDetail)
        {
            if (details.Key.GetID() == player.GetID())
            {
                playerHealth += details.Value.GetHealth();
            }
        }
        return playerHealth;
    }

    public List<IPlayer> GetAlivePlayers()
    {
        if (playerDetail == null)
        {
            throw new NullReferenceException(message : "No Added Player Yet!");
        }

        List<IPlayer> alivePlayers = new();
        foreach (var details in playerDetail)
        {
            int playerHealthPoint = details.Value.GetHealth();

            if (playerHealthPoint > 0)
            {
                alivePlayers.Add(details.Key);
            }
        }
        return alivePlayers;
    }
}
