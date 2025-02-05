using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Village
{
    public class Barrack : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Barrack;

        public List<SoldierInfo> AvailableSoldiers { get; private set; }

        private void Start()
        {
            // Üretilebilecek asker türleri (isteðe baðlý olarak bu liste geniþletilebilir)
            AvailableSoldiers = new List<SoldierInfo>
            {
                new SoldierInfo(SoldierType.Suvari, 35),
                new SoldierInfo(SoldierType.Yaya, 35)
            };
        }

        public override string GetData()
        {
            return JsonUtility.ToJson(new Data()
            {
                AvailableSoldiers = AvailableSoldiers
            });
        }

        public override void SetData(string dataString)
        {
            Data data = JsonUtility.FromJson<Data>(dataString);
            AvailableSoldiers = data.AvailableSoldiers;
        }

        [Serializable]
        private struct Data
        {
            public List<SoldierInfo> AvailableSoldiers;
        }

        public override void OnPress()
        {
            ShowPanel();
        }

        

        [Serializable]
        public class SoldierInfo
        {
            public SoldierType Type;
            public int Count;

            public SoldierInfo(SoldierType type, int count)
            {
                Type = type;
                Count = count;
            }
        }
    }

    public enum SoldierType
    {
        Suvari,
        Yaya
    }
}
