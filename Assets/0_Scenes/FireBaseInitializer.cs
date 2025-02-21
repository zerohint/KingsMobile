using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    public FirebaseManager firebaseManager;

    private void Awake()
    {
        if (firebaseManager != null)
        {
            firebaseManager.InitializeFirebase();
        }
        else
        {
            Debug.LogError("FirebaseManager reference not assigned!");
        }
    }
}
