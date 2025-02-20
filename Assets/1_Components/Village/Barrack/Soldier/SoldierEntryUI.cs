using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoldierEntryUI : MonoBehaviour
{
    [SerializeField] private Image soldierIcon;
    [SerializeField] private TextMeshProUGUI soldierNameText;
    [SerializeField] private TextMeshProUGUI soldierCountText;
    [SerializeField] private Button produceButton;

    private SoldierData soldierData;
    private System.Action<SoldierData, SoldierEntryUI> onProduceClicked;

    public void Setup(SoldierData data, System.Action<SoldierData, SoldierEntryUI> onProduce)
    {
        soldierData = data;
        soldierNameText.text = soldierData.soldierName;
        soldierCountText.text = soldierData.initialCount.ToString();
        soldierIcon.sprite = soldierData.icon;
        onProduceClicked = onProduce;
        produceButton.onClick.AddListener(() => onProduceClicked?.Invoke(soldierData, this));
    }

    public void UpdateCount(int newCount)
    {
        soldierCountText.text = newCount.ToString();
    }

    public Sprite GetSoldierIcon()
    {
        return soldierIcon.sprite;
    }
}
