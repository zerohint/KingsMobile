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

        /// <summary>
        /// Performs the building upgrade process.
        /// Common logic for all buildings can be written here.
        /// If you want a special behavior, you can override it.
        /// </summary>
        public virtual void Upgrade()
        {
            currentUpgradeStage++;
            Debug.Log($"{name} {currentUpgradeStage}. upgraded.");
            // Common upgrade operations (e.g. resource consumption, stat increase, etc.) can be added here.
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
