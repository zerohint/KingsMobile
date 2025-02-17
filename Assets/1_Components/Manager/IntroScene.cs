using UnityEngine;

public class IntroScene : MonoBehaviour
{
    public void Start()
    {
        ScenesManager.Scene targetScene = PlayersManager.Instance.playerData.IsLanded
            ? ScenesManager.Scene.Game
            : ScenesManager.Scene.Landing;
        ScenesManager.Instance.LoadScene(targetScene);
    }
}
