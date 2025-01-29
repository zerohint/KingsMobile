using UnityEngine;

namespace Game.Village
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        [SerializeField] private MapPanel mapPanel;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowBuildingPanel(MapObjectBase obj)
        {
            mapPanel.UpdatePanel(obj.GetPanelData());
            mapPanel.SetActive(true);
        }

        public void HideBuildingPanel()
        {
            mapPanel.SetActive(false);
        }
    }
}
