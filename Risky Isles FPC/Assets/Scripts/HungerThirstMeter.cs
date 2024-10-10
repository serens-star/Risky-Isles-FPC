using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerThirstMeter : MonoBehaviour
{
    public Slider hungerSlider;
    public Slider thirstSlider;
    //public PlayerStats playerStats;

    public void SetThirst(float thirst)
    { 
        
        thirstSlider.value = thirst;
        Debug.Log("Thirst Level: " + thirst);
        //Canvas.ForceUpdateCanvases();
    }

   /* public void SetSliderMax(int thirst)
    {
        thirstSlider.maxValue = thirst;
        SetSliderMax(thirst);
    } */

    public void SetHunger(float hunger)
    {
        hungerSlider.value = hunger;
        Debug.Log("Hunger Level: " + hunger);
        //Canvas.ForceUpdateCanvases();
    }
   
    public void SetMaxThirst(float thirst)
     {
        thirstSlider.maxValue = thirst;
       // SetMaxThirst(thirst);
        thirstSlider.value = thirst;
     }
    
    public void SetMaxHunger(float hunger)
     {
         hungerSlider.maxValue = hunger;
         hungerSlider.value = hunger;
     }
    
    
}
