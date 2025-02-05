using System;
using UnityEngine;

namespace Game.Village
{
    public class Farm : BuildingBase
    {
        public override BuildingType BuildingType => BuildingType.Farm;
        public override void OnPress()
        {
            ShowPanel();
            Debug.Log("farm press");
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