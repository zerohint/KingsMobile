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

}
