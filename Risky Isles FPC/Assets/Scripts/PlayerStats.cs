using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HungerThirstMeter hungerScript;
  
  
    public int maxThirst = 100;
    public int minThirst = 0;

    public int maxHunger = 100;
    public int minHunger = 0;

    public int currentThirst;

    public int currentHunger;
    public float thirstDecayRate = 1f;
    public float hungerDecayRate = 0.75f;
    
    public void Start()
    {
        currentThirst = maxThirst;
        currentHunger = maxHunger;
        
        hungerScript.SetMaxThirst(maxThirst);
        hungerScript.SetMaxHunger(maxHunger);

    }
    
    public void Update()
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
    }

    private void DecreaseThirst(float amount)
    {
        currentThirst -= Mathf.FloorToInt(amount);
        currentThirst = Mathf.Clamp(currentThirst, minThirst, maxThirst);
        hungerScript.SetThirst(currentThirst);
        Debug.Log("Thirst Decreased to: " + currentThirst);
    }

    private void DecreaseHunger(float amount)
    {
        currentHunger -= Mathf.FloorToInt(amount);
        currentHunger = Mathf.Clamp(currentHunger, minHunger, maxHunger);
        hungerScript.SetHunger(currentHunger);
        Debug.Log("Hunger Decreased to: " + currentHunger);
    }

    // Method to increase thirst
    public void IncreaseThirst(int amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);  // Clamp to avoid exceeding max value
        Debug.Log("Thirst increased: " + currentThirst);
        
        hungerScript.SetThirst(currentThirst);
    }

    // Method to increase hunger
    public void IncreaseHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);  // Clamp to avoid exceeding max value
        Debug.Log("Hunger increased: " + currentHunger);
        
        hungerScript.SetHunger(currentHunger);
    }
}