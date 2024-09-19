using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string mainScene = "SampleScene";
    public void ClickStart()
    {
        SceneManager.LoadSceneAsync(mainScene);
    }
    
    void Update()
    {
        
    }
}