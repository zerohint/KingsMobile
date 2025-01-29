using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingBase : MonoBehaviour, IPressObject
    {
        public abstract void OnPress();
        public abstract System.Type GetPanelType();

        protected void ShowPanel()
        {
            BuildingManager.Instance.ShowBuildingPanel(this);
        }
    }
}
