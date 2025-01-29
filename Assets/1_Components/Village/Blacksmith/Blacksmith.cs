namespace Game.Village
{
    public class Blacksmith : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(BlacksmithPanel);
        }
    }
}