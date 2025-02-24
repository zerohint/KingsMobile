using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZeroGame;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text grainText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private Image avatarImage;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (PlayersManager.Instance == null || PlayersManager.Instance.playerData == null)
            return;

        var data = PlayersManager.Instance.playerData;

        coinText.text = " " + data.coin;
        gemText.text = " " + data.gem;
        grainText.text = " " + data.grain;

        playerNameText.text = data.playerName;
        playerLevelText.text = data.playerLevel.ToString();

        AvatarDataSC avatarData = SCDB.Get<AvatarDataSC>(avatar => avatar.Id == data.avatarId);
        if (avatarData != null)
        {
            avatarImage.sprite = avatarData.Photo;
        }
    }
}
