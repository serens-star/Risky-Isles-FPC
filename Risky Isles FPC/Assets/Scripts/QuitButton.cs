using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public string startMenuScene = "StartMenu";  
    
    public void OnQuitButtonClick()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (currentScene.name == "SampleScene")
        {
            SceneManager.LoadScene(startMenuScene);
            Debug.Log("Returning to Start Menu");
        }
        else
        {
            Debug.LogWarning("Not in SampleScene, cannot quit to Start Menu");
        }
    }
}
