using UnityEngine;

[CreateAssetMenu(fileName = "AvatarData", menuName = "Game/AvatarData")]
public class AvatarDataSC : ScriptableObject
{
    [field:SerializeField] public int Id { get; private set; }
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public Sprite Photo { get; private set; }
}
