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

        [SerializeField]
        protected int currentUpgradeStage = 1;
        public int CurrentUpgradeStage => currentUpgradeStage;


        public virtual void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log($"{name} {currentUpgradeStage}. upgraded.");        }

        public virtual UpgradeStage GetNextUpgradeStage()
        {
            return null;
        }

        public virtual string GetUpgradeInfo()
        {
            return string.Empty;
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
