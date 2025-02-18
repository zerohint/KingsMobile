using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "ScenesManager", menuName = "Game/Managers/Scenes Manager")]
public sealed class ScenesManager : SingletonSC<ScenesManager>
{
    public Scene CurrentScene => (Scene)SceneManager.GetActiveScene().buildIndex;

    /// <summary>
    /// Load scene by loading panel
    /// </summary>
    /// <param name="scene"></param>
    public async void LoadScene(Scene scene)
    {
        //LoadingPanel.SetLoading(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene);
        while (!asyncLoad.isDone)
            await Task.Yield();
        
        //LoadingPanel.SetLoading(false);
    }

    public enum Scene
    {
        Intro = 1,
        Game = 3,
        Landing = 2
    }
}
