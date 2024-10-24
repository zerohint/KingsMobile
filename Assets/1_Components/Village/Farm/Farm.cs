using UnityEngine;

namespace Game.Village
{
    public class Farm : VillageBuilding
    {
        public override void OnPress()
        {
            Debug.Log("farm press");
        }
    }
}