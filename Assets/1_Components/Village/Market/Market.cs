using System;
using UnityEngine;

namespace Game.Village
{
    public class Market : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Market;
        public override void OnPress()
        {
            ShowPanel();
        }
        public override string GetData()
        {
            return JsonUtility.ToJson(new Data()
            {
            });
        }

        public override void SetData(string dataString)
        {
            Data data = JsonUtility.FromJson<Data>(dataString);
        }
        [Serializable]
        private struct Data
        {
        }
    }
}