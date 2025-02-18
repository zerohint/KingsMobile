using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapObject : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Intro");
    }
}
