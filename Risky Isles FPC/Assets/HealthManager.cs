using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class HealthManager : MonoBehaviour
{
    public Slider HealthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isChoking = false;
    [SerializeField] private bool isDead = false;

    public GameObject gameOverPanel;
    public AudioSource GameOverAudioSource;
    
    //Green Panel shows to give gas effect
    public GameObject GasBlindness;

    private int GC;//Gas Choke

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = currentHealth;
        GasBlindness.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone") && GC == 0)
        {
            GC++;
            StartCoroutine(GasBlindnesseffect());
            isChoking = true;
        }
        
    }

    IEnumerator GasBlindnesseffect()
    {
        GasBlindness.SetActive(true);
        yield return new WaitForSeconds(1);
        GasBlindness.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine(GasBlindnesseffect());
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone") && GC == 1)
        {
            GC--;
            isChoking = false;
        }
        
    }
    public void Update()
    {
        if (isDead) return;

        HealthBar.value = currentHealth;

        if (isChoking)
        {
            currentHealth -= 0.0075f;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        if (GC == 0)
        {
            GasBlindness.SetActive(false);
            StopAllCoroutines();

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

   
}*/
