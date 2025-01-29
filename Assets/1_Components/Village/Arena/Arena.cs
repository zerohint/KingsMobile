namespace Game.Village
{
    public class Arena : BuildingBase
    {
        public override void OnPress()
        {
            ShowPanel();
        }
        public override System.Type GetPanelType()
        {
            return typeof(ArenaPanel);
        }
    }
}
