using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialProgress : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timeText; // Yeni eklenen süre göstergesi

    public void UpdateProgress(float progress, float timeLeft)
    {
        fillImage.fillAmount = progress;
        UpdateTimeText(timeLeft);
    }

    private void UpdateTimeText(float timeLeft)
    {
        int hours = Mathf.FloorToInt(timeLeft / 3600);
        int minutes = Mathf.FloorToInt((timeLeft % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        if (hours > 0)
        {
            timeText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else
        {
            timeText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
