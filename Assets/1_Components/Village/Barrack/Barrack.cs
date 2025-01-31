using System.Collections.Generic;
using UnityEngine;

namespace Game.Village
{
    public class Barrack : BuildingBase
    {
        public List<SoldierType> AvailableSoldiers { get; private set; }

        private void Start()
        {
            // Üretilebilecek asker türleri (isteðe baðlý olarak bu liste geniþletilebilir)
            AvailableSoldiers = new List<SoldierType>
            {
                new SoldierType("Süvari", 35),
                new SoldierType("Atsýz", 35),
                new SoldierType("Piyade", 35)
            };
        }

        private void LoadData()
        {
            int soldierCount = GameManager.Instance.unitCounts.ContainsKey("soldier")
                ? GameManager.Instance.unitCounts["soldier"]
                : 0;

            Debug.Log("Barracks has " + soldierCount + " soldiers.");
        }

        public override void OnPress()
        {
            ShowPanel();
        }

        public override System.Type GetPanelType()
        {
            return typeof(BarrackPanel);
        }
    }

    public class SoldierType
    {
        public string Name;
        public int Count;

        public SoldierType(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
