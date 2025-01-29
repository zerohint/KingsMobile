namespace Game.Village
{
    public class Stable : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(StablePanel);
        }
    }
}