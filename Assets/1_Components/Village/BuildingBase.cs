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