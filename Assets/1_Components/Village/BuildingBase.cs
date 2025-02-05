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
        }
    }

    public enum BuildingType
    {
        Barrack,
        House,
        Market,
        Mine,
        Sawmill,
        StoneMine,
        WoodCutter
    }
}