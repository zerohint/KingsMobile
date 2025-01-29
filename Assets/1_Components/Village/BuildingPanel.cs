using UnityEngine;
using UnityEngine.UI;

namespace Game.Village
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] private Text buildingNameText;
        [SerializeField] private GameObject panelGO;
        [SerializeField] private Canvas canvas;

        private BuildingBase currentBuilding;

        public void UpdatePanel(BuildingBase building)
        {
            currentBuilding = building;
            buildingNameText.text = building.name;
        }

        public void SetActive(bool active)
        {
            canvas.enabled = active;
            panelGO.SetActive(active);

            if (!active)
            {
                currentBuilding = null;
            }
        }

        public BuildingBase GetCurrentBuilding()
        {
            return currentBuilding;
        }
    }
}
