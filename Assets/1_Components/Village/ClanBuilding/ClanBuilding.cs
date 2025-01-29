namespace Game.Village
{
    public class ClanBuilding : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(ClanBuildingPanel);
        }

    }
}