using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private Controls pauseControls;
    public static PauseManager instance;
    [Header("PauseAssets")] 
    public GameObject pausePanel;

    public GameObject controlsPanel;
    public bool isPaused = false;
    
    [Header("Buttons")] 
    [Space(2)] 
    public Button resume;
    public Button controls;
    public Button closeControls;
    public Button restartGame;
    public Button quit;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        pauseControls = new Controls();

        pauseControls.Pause.Unpause.performed += ctx => ResumeGame();
    }

    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        resume.onClick.AddListener(ResumeGame);
        controls.onClick.AddListener(ShowControls);
        closeControls.onClick.AddListener(HideControls);
        restartGame.onClick.AddListener(BackToStartScene);
        quit.onClick.AddListener(QuitGame);
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        FirstPersonControls.Instance.PlayerInput.Disable();
        pauseControls.Pause.Enable();     
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        FirstPersonControls.Instance.PlayerInput.Enable();
        pauseControls.Pause.Disable();
        isPaused = false;
    }

    public void BackToStartScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}
