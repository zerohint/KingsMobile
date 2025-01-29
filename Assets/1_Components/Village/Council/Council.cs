namespace Game.Village
{
    public class Council : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(CouncilPanel);
        }
    }
}