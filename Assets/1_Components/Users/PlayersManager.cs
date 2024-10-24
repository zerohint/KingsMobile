using UnityEngine;

[CreateAssetMenu(fileName = "PlayersManager", menuName = "Game/Managers/Players Manager")]
public class PlayersManager : SingletonSC<PlayersManager>
{
    public PlayerPublicData playerPublicData;

    // other players

    

    public override void Initialize()
    {
        base.Initialize();

        playerPublicData = PlayerPublicData.LoadLocal();
    }
}
