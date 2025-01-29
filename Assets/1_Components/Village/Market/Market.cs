namespace Game.Village
{
    public class Market : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(MarketPanel);
        }
    }
}