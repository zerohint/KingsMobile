using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier", menuName = "Game/Soldier")]
public class SoldierData : ScriptableObject
{
    public string soldierName;
    public Sprite icon;
    public float productionTime;
    public int initialCount;
}
