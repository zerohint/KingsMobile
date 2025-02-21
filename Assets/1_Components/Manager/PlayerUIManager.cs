using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text grainText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (PlayersManager.Instance == null || PlayersManager.Instance.playerData == null)
            return;

        var data = PlayersManager.Instance.playerData;
        playerNameText.text = data.playerName;
        playerLevelText.text = "Level: " + data.playerLevel;
        coinText.text = " " +data.coin;
        gemText.text = " " + data.gem;
        grainText.text = " " + data.grain;
    }
}
