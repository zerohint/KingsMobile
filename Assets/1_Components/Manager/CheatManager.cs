using UnityEngine;
using UnityEngine.InputSystem;

public class CheatManager : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
        {
            ApplyCheat();
        }
    }

    private void ApplyCheat()
    {
        var playerData = PlayersManager.Instance.playerData;

        playerData.coin += 10000;
        playerData.gem += 10000;
        playerData.grain += 10000;

        FirebaseManager.Instance.SavePlayerData(playerData, () =>
        {
            Debug.Log("Cheated.");
        });

        PlayerUIManager uiManager = FindObjectOfType<PlayerUIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateUI();
        }
    }
}
