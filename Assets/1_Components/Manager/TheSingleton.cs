using UnityEngine;

public sealed class TheSingleton : MonoBehaviour
{
    public static TheSingleton Instance { get; private set; }
    [SerializeField, Tooltip("SingletonSC managers")]
    private SingletonSCNonGeneric[] managers;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var manager in managers)
            manager.Initialize();
    }


    /// <summary>
    /// Get singleton manager by type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetManager<T>() where T : SingletonSC<T>
    {
        Debug.Assert(Instance != null, "TheSingleton instance not found. Did you reached at awake?");

        foreach (var manager in Instance.managers)
            if (manager is T t)
                return t;
        return null;
    }
}
