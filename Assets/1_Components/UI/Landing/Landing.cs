using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Landing : MonoBehaviour
{
    [SerializeField] private LoginPanel loginManager;
    [Space]
    [SerializeField] private Selection feudatorySelection;
    [SerializeField] private Selection avatarSelection;
    [SerializeField] private TMP_InputField userName;
    // [SerializeField] private Selection emblemSelection;

    private void Start()
    {
        loginManager.OnLoginDone.AddListener(OnLoginDone);
    }

    private void OnLoginDone()
    {
        var pm = PlayersManager.Instance;

        if (string.IsNullOrEmpty(pm.playerData.playerName))
        {
            pm.playerData = new PlayerData(userName.text);
        }
        else
        {
            pm.playerData.playerName = userName.text;
        }

        if (pm.playerData.playerLevel == 0)
        {
            pm.playerData.playerLevel = 1;
            pm.playerData.coin = 1000;
            pm.playerData.gem = 0;
            pm.playerData.grain = 500;
        }

        pm.playerData.feudetoryId = (feudatorySelection as FeudatorySelection).feudatories[feudatorySelection.Value].Id;
        pm.playerData.avatarId = (avatarSelection as AvatarSelection).avatars[avatarSelection.Value].Id;

        LoginManager.Instance.SetUserData(new LoginManager.Data(userName.text));

        string dataJson = JsonUtility.ToJson(pm.playerData);
        PlayerPrefs.SetString("PlayerPublicData", dataJson);
        PlayerPrefs.Save();

        // Firestore kaydý da yapýlýyor.
        FirebaseManager.Instance.SavePlayerData(pm.playerData, () =>
        {
            Debug.Log("Landing: Player data saved to Firestore.");
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Game);
        });
    }
}
