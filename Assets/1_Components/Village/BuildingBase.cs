using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingBase : MonoBehaviour, IPressObject
    {
        public abstract BuildingType BuildingType { get; }
        public abstract void OnPress();
        public abstract string GetData();
        public abstract void SetData(string data);

        protected void ShowPanel()
        {
            LeftPanel.Instance.OpenPanel(BuildingType);
            LeftPanel.Instance.UpdatePanel(this);
        }

        // T�m binalarda ortak upgrade a�amas�
        [SerializeField]
        protected int currentUpgradeStage = 1;
        public int CurrentUpgradeStage => currentUpgradeStage;

        /// <summary>
        /// Bina y�kseltme i�lemini ger�ekle�tirir.
        /// T�m binalar i�in ortak mant�k buraya yaz�labilir.
        /// E�er �zel bir davran�� isterseniz, override edebilirsiniz.
        /// </summary>
        public virtual void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log($"{name} {currentUpgradeStage}. a�amaya y�kseltildi.");
            // Ortak y�kseltme i�lemleri (�r. kaynak t�ketimi, stat art��� vs.) buraya eklenebilir.
        }
    }

    public enum BuildingType
    {
        Arena,
        Barrack,
        Blacksmith,
        Center,
        ClanBuilding,
        Council,
        Farm,
        Market,
        Stable,
    }
}
