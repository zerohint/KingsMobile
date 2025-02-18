using UnityEngine;

[CreateAssetMenu(fileName = "PlayersManager", menuName = "Game/Managers/Players Manager")]
public class PlayersManager : SingletonSC<PlayersManager>
{
    public bool IsDataLoaded { get; private set; }
    public PlayerData playerData;

    public void LoadData()
    {
        string dataJson = PlayerPrefs.GetString("PlayerPublicData", "{}");
        if (dataJson != "{}")
        {
            playerData = JsonUtility.FromJson<PlayerData>(dataJson);
            IsDataLoaded = true;
        }
        else
        {
            playerData = new PlayerData();
        }
    }
}
