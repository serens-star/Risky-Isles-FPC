using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string mainScene = "SampleScene";
    public void ClickStart()
    {
        SceneManager.LoadSceneAsync(mainScene);
    }

    public void ClickQuit()
    {
        Application.Quit();
        Debug.Log("No Game for You");
    }

    void Update()
    {
        
    }
}