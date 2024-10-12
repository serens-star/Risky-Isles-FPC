using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public HungerThirstMeter hungerThirstScript;
    private Coroutine hungerThirstCoroutine; 
   
  
  
    public float maxThirst = 100;
    public float minThirst = 0;

    public float maxHunger = 100;
    public float minHunger = 0;

    public float currentThirst;

    public float currentHunger;

    public GameObject gameOverPanel;

    public AudioSource GameOverAudioSource;
    //public float thirstDecayRate = 1f;
    //public float hungerDecayRate = 0.75f;

    private bool isDead = false; 
    //private bool statsInitialized = false;
    
    void Start()
    {
      // InitializeStats();
       currentThirst = maxThirst;
       //currentThirst = maxThirst;
       currentHunger = maxHunger;
            
       hungerThirstScript.SetMaxThirst(maxThirst);
       hungerThirstScript.SetMaxHunger(maxHunger);

       hungerThirstCoroutine = StartCoroutine(HungerThirstDrainRoutine());
    }

    IEnumerator HungerThirstDrainRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(7); //decrease hunger very 2secs
            //if (!isFood) //remove hunger when player has no food
            {
                DecreaseHunger(2); //amount of hunger that decrases
                DecreaseThirst(4);//amount of thirt secrease
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
            Die();
        }
    }

    private void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("You're Dead!");
            if (GameOverAudioSource != null)
            {
                GameOverAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("Womp Womp");
            }

            gameOverPanel.SetActive(true);
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

    /*public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }*/

    public void Retry()
    {
        Time.timeScale = 1;
        StopAllCoroutines();
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