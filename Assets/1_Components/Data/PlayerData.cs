public class PlayerData
{
    public string playerName;
    public int playerLevel = 0;
    public int gold = 0;
    public int food = 0;

    public PlayerData() { }

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        // Yeni oyuncu için varsayýlan deðerleri atayabilirsiniz
        playerLevel = 1;
        gold = 1000;
        food = 500;
    }
}