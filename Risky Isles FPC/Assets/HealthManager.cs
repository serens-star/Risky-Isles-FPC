using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider HealthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isChoking = false;
    [SerializeField] private bool isDead = false;

    public GameObject gameOverPanel;
    public AudioSource GameOverAudioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = currentHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone"))
        {
            isChoking = true;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone"))
        {
            isChoking = false;
        }
        
    }
    public void Update()
    {
        if (isDead) return;

        HealthBar.value = currentHealth;

        if (isChoking)
        {
            currentHealth -= 0.01f;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("(UI)You're Dead!");
            
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                Debug.Log("G.O. Panel Activated");
            }
            else
            {
                Debug.LogWarning("Game Over Panel Isn't Assigned");
            }

            if (GameOverAudioSource != null)
            {
                GameOverAudioSource.Play();
                Debug.Log("G.O. Audio Played");
            }
            else
            {
                Debug.LogWarning("Game Over Audio Isn't Assigned");
            }
            
            Time.timeScale = 0; //pause ganme
        }
    
    }

   
}
