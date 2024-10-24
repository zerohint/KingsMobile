using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class Army : MonoBehaviour, IPressObject
    {
        [SerializeField] private LayoutGroup starNest;
        [SerializeField] private ArmyStar armyStarPrefab;

        public void OnPress()
        {
            Debug.Log("army pressed");
        }

        public void Visualize(int soldierCount)
        {
            
            for (int i = 0; i < soldierCount; i++)
            {
                var star = Instantiate(armyStarPrefab, starNest.transform);
            }
        }



        private static int GetStarCount(int asker)
        {
            return asker switch
            {
                <= 50 => 0,
                <= 200 => 1,
                <= 500 => 2,
                <= 1000 => 3,
                <= 5000 => 4,
                <= 20000 => 5,
                _ => -1
            };
        }

        

        public class Data
        {
            public int soldierCount;

           
        }
    }
}