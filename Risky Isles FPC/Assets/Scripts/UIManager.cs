using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string mainScene = "SampleScene";
    public GameObject aboutPanel;
    public void ClickStart()
    {
        SceneManager.LoadSceneAsync(mainScene);
    }

    public void ClickQuit()
    {
        Application.Quit();
        Debug.Log("No Game for You"); //This is for testing purposes 
    }

    public void ClickAbout()
    {
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
    }

    void Update()
    {
        
    }
}