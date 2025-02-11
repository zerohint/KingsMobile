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

        // Tüm binalarda ortak upgrade aþamasý
        [SerializeField]
        protected int currentUpgradeStage = 1;
        public int CurrentUpgradeStage => currentUpgradeStage;

        /// <summary>
        /// Bina yükseltme iþlemini gerçekleþtirir.
        /// Tüm binalar için ortak mantýk buraya yazýlabilir.
        /// Eðer özel bir davranýþ isterseniz, override edebilirsiniz.
        /// </summary>
        public virtual void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log($"{name} {currentUpgradeStage}. aþamaya yükseltildi.");
            // Ortak yükseltme iþlemleri (ör. kaynak tüketimi, stat artýþý vs.) buraya eklenebilir.
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
