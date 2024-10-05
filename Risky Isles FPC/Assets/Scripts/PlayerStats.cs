using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HungerThirstMeter hungerScript;
  
  
    public int maxThirst = 100;
    public int minThirst;

    public int maxHunger = 100;
    public int minHunger;

    public int currentThirst;

    public int currentHunger;
    
    public void Start()
    {
        currentThirst = minThirst;
        currentHunger = minHunger;
        
        // hungerScript.SetMaxThirst(minThirst);
        // hungerScript.SetMaxHunger(minHunger);

    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IncreaseThirst(20);   // Increase thirst by 10 when pressing space
            IncreaseHunger(20);   // Increase hunger by 10 when pressing space
        }
    }

    // Method to increase thirst
    private void IncreaseThirst(int amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);  // Clamp to avoid exceeding max value
        Debug.Log("Thirst increased: " + currentThirst);
        
        hungerScript.SetThirst(currentThirst);
    }

    // Method to increase hunger
    private void IncreaseHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);  // Clamp to avoid exceeding max value
        Debug.Log("Hunger increased: " + currentHunger);
        
        hungerScript.SetHunger(currentHunger);
    }
}