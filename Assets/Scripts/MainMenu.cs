using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SwitchScene()
    {
        // Logic to switch to another scene
        SceneManager.LoadScene("Office"); 
    }

    public void QuitApplication()
    {
        // Logic to quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}