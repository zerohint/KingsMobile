using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Village
{
    public class SoldierEntryUI : MonoBehaviour
    {
        [SerializeField] private Image soldierIcon;
        [SerializeField] private TextMeshProUGUI soldierNameText;
        [SerializeField] private TextMeshProUGUI soldierCountText;
        [SerializeField] private Button produceButton;

        private Barrack.SoldierInfo soldierInfo;
        private System.Action onProduceClicked;

        public void Setup(Barrack.SoldierInfo soldier, System.Action onProduce)
        {
            soldierInfo = soldier;
            soldierNameText.text = soldier.Type.ToString();
            soldierCountText.text = soldier.Count.ToString();
            soldierIcon.sprite = soldier.Icon;
            onProduceClicked = onProduce;
            produceButton.onClick.AddListener(() => onProduceClicked?.Invoke());
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
}
