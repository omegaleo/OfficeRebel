using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasToCamera : MonoBehaviour
{
    private Canvas _canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        _canvas = GetComponent<Canvas>();
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _canvas.worldCamera = Camera.main;
    }
}
