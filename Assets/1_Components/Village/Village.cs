using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game.Village;

public class Village : MonoBehaviour
{
    [SerializeField] private BuildingBase[] buildings;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => PlayersManager.Instance.IsDataLoaded);

        if (PlayersManager.Instance.playerData.villageData.buildingsData != null &&
            PlayersManager.Instance.playerData.villageData.buildingsData.Length > 0)
        {
            SetData(PlayersManager.Instance.playerData.villageData);
        }
        else
        {
            Debug.Log("Village verisi bulunamadý, varsayýlan ayarlar kullanýlacak.");
        }
    }


    public Data GetData()
    {
        Data data = new Data();
        data.buildingsData = new string[buildings.Length];
        for (int i = 0; i < buildings.Length; i++)
        {
            data.buildingsData[i] = buildings[i].GetData();
        }
        return data;
    }


    public void SetData(Data data)
    {
        for (int i = 0; i < buildings.Length && i < data.buildingsData.Length; i++)
        {
            buildings[i].SetData(data.buildingsData[i]);
        }
    }

    [Serializable]
    public struct Data
    {
        public string[] buildingsData;
    }
}