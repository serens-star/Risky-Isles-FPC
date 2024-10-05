using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerThirstMeter : MonoBehaviour
{
    public Slider hungerSlider;
    public Slider thirstSlider;
    public PlayerStats playerStats;

    public void SetThirst(int thirst)
    { 
        
        thirstSlider.value = thirst;
    }

    public void SetHunger(int hunger)
    {
        hungerSlider.value = hunger;
    }
   
    // public void SetMaxThirst(int thirst)
    // {
    //     thirstSlider.maxValue = thirst;
    //     thirstSlider.value = thirst;
    // }
    //
    // public void SetMaxHunger(int hunger)
    // {
    //     hungerSlider.maxValue = hunger;
    //     hungerSlider.value = hunger;
    // }
    
}
