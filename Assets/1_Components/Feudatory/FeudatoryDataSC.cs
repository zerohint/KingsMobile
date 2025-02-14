using Game.Map;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FeudatoryData", menuName = "Game/FeudatoryData")]
public class FeudatoryDataSC : ScriptableObject
{
    [field:SerializeField] public int Id { get; private set; }
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Slogan { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }
    [field:SerializeField, TextArea] public string Description { get; private set; }
    [field:SerializeField] public Color ThemeColor { get; private set; }
    [field:SerializeField] public Castle[] Castles { get; private set; } = new Castle[4];
    [field: SerializeField] public Coordinate CenterPosition { get; set; }


    [Serializable]
    public struct Castle
    {
        public Coordinate Coordinate;
    }

}
