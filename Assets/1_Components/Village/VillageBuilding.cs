using UnityEngine;

namespace Game.Village
{
    public abstract class VillageBuilding : MonoBehaviour, IPressObject
    {
        public abstract void OnPress();
    }
}