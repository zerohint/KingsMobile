using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game.Village
{
    public class SoldierEntryUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text soldierNameText;
        [SerializeField] private TMP_Text soldierCountText;
        [SerializeField] private Button recruitButton;

        private SoldierType soldier;
        private System.Action<SoldierType> onRecruitCallback;

        public void Setup(SoldierType soldier, System.Action<SoldierType> onRecruit)
        {
            this.soldier = soldier;
            this.onRecruitCallback = onRecruit;

            soldierNameText.text = soldier.Name;
            soldierCountText.text = soldier.Count.ToString();

            recruitButton.onClick.RemoveAllListeners();
            recruitButton.onClick.AddListener(() =>
            {
                onRecruitCallback?.Invoke(soldier);
                soldierCountText.text = soldier.Count.ToString(); // UI güncelle
            });
        }
    }
}
