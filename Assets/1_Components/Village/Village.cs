using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Village;

public class Village : MonoBehaviour
{
    [SerializeField] private BuildingBase[] buildings;

    public string GetData()
    {
        List<string> datas = new List<string>();
        foreach (var building in buildings)
        {
            datas.Add(building.GetData());
        }
        return JsonUtility.ToJson(new Data { buildingsData = datas.ToArray() });
    }

    public void SetData(string data)
    {
        Data loadedData = JsonUtility.FromJson<Data>(data);
        for (int i = 0; i < buildings.Length && i < loadedData.buildingsData.Length; i++)
        {
            buildings[i].SetData(loadedData.buildingsData[i]);
        }
    }

    [Serializable]
    private struct Data
    {
        public string[] buildingsData;
    }
}
