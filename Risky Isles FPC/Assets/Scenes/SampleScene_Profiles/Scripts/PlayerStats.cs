using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HungerThirstMeter hungerThirstScript;
    public Slider healthSlider;
    private Coroutine hungerThirstCoroutine; 
   
  
  
    public float maxThirst = 100;
    public float minThirst = 0;

    public float maxHunger = 100;
    public float minHunger = 0;
    
    public float maxHealth = 100;
    public float minHealth = 0;

    public float currentThirst;
    public float currentHunger;
    public float currentHealth;

    public GameObject gameOverPanel;
    public AudioSource GameOverAudioSource;
    //public float thirstDecayRate = 1f;
    //public float hungerDecayRate = 0.75f;

    private bool isDead = false; 
    //private bool statsInitialized = false;
    
    void Start()
    {
       currentThirst = maxThirst;
       currentHunger = maxHunger;
       currentHealth = maxHealth;
            
       hungerThirstScript.SetMaxThirst(maxThirst);
       hungerThirstScript.SetMaxHunger(maxHunger);

       if (healthSlider != null)
       {
           healthSlider.maxValue = maxHealth;
           healthSlider.value = currentHealth;
       }

       StartCoroutine(HungerThirstDrainRoutine());

       //hungerThirstCoroutine = StartCoroutine(HungerThirstDrainRoutine());
    }

    IEnumerator HungerThirstDrainRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(6.5f); //decrease hunger very 2secs
            //if (!isFood) //remove hunger when player has no food
            {
                DecreaseHunger(0.00001f); //amount of hunger that decrases
                DecreaseThirst(4f);//amount of thirt secrease
            }
        }
    }

    private void DecreaseHunger(float amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Clamp(currentHunger, minHunger, maxHunger);
        Debug.Log("Hunger Decrease to: " + currentHunger);
        hungerThirstScript.SetHunger(currentHunger);

        if (currentHunger <= 0 && !isDead)
        {
            Die();
        }

       /* if (currentHunger <= 0 || currentThirst <= 0)
        {
            currentHunger = 0;
            currentThirst = 0; 

            HungerThirstMeter.HungerSlider(currentHunger);
            HungerThirstMeter.HungerSlider(currentHunger);
            
        } */
    }

    private void DecreaseThirst(float amount)
    {
        currentThirst -= amount;
        currentThirst = Mathf.Clamp(currentThirst, minThirst, maxThirst);
        Debug.Log("Thirst Decrease to: " + currentThirst);
        hungerThirstScript.SetThirst(currentThirst);

        if (currentThirst <= 0 && !isDead)
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return;
        
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log("Current Health: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
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
    
    // Method to increase thirst
    public void IncreaseThirst(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);  // Clamp to avoid exceeding max value
        Debug.Log("Thirst increased: " + currentThirst);
        
        hungerThirstScript.SetThirst(currentThirst);
    }

    // Method to increase hunger
    public void IncreaseHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);  // Clamp to avoid exceeding max value
        Debug.Log("Hunger increased: " + currentHunger);
        
        hungerThirstScript.SetHunger(currentHunger);
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        Debug.Log("Health increased: " + currentHealth);
    }

    /*public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }*/

    public void Retry()
    {
        Time.timeScale = 1;
        StopAllCoroutines();
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*private void StartHungerThirstDrain()
    {
        if (healthThirstRoutineCoroutine != null)
        {
            StopCoroutine(healthThirstRoutineCoroutine);
        }

        healthThirstRoutineCoroutine = StartHungerThirstDrain(HungerThirstDrainRoutine()); 
    }
  /*  void Update()
    {
        DecreaseThirst(Time.deltaTime * thirstDecayRate);
        DecreaseHunger(Time.deltaTime * hungerDecayRate);
        
        Debug.Log("Current Thirst: " + thirstDecayRate);
        Debug.Log("Current Hunger: " + hungerDecayRate);
        
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        // IncreaseThirst(20);   // Increase thirst by 20 when pressing t
        //}

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //IncreaseHunger(20);
        //}
    } */

    /*private void InitializeStats()
    {
        if (!statsInitialized)
        {
            currentThirst = maxThirst;
            currentHunger = maxHunger;
            
            hungerScript.SetMaxThirst(maxThirst);
            hungerScript.SetMaxHunger(maxHunger);

            statsInitialized = true;
        }
    } */ 

   /* private void DecreaseThirst(float amount)
    {
       /* currentThirst -= Mathf.FloorToInt(amount);
        currentThirst = Mathf.Clamp(currentThirst, minThirst, maxThirst);
        Debug.Log("Thirst Decreased to: " + currentThirst);
        hungerThirstScript.SetThirst(currentThirst);*/
       
       
   /* }

    private void DecreaseHunger(float amount)
    {
        currentHunger -= Mathf.FloorToInt(amount);
        currentHunger = Mathf.Clamp(currentHunger, minHunger, maxHunger);
        Debug.Log("Hunger Decreased to: " + currentHunger);
        hungerThirstScript.SetHunger(currentHunger);
    }*/

    
}