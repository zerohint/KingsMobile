using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingBase : MonoBehaviour, IPressObject
    {
        public abstract void OnPress();
        public abstract System.Type GetPanelType();
        public abstract string GetData();
        public abstract void SetData(string data);

        protected void ShowPanel()
        {
            BuildingManager.Instance.ShowBuildingPanel(this);
        }
    }
}
