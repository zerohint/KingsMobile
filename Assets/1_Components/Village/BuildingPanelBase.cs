using UnityEngine;

namespace Game.Village
{
    public abstract class BuildingPanelBase : MonoBehaviour
    {
        [SerializeField]
        private BuildingType buildingType;
        public BuildingType BuildingType => buildingType;

        public BuildingBase Building { get; private set; }

        public virtual void SetBuilding(BuildingBase building)
        {
            Building = building;
        }

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void Initialize(BuildingType type)
        {
            buildingType = type;
        }
    }
}
