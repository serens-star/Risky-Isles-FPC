using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public bool isFood;
    public bool isWater;
    public bool isHealth;
    public int hungerRestoreAmount;
    public int thirstRestoreAmount;
    public int healthRestoreAmount;

    public void Consume(PlayerStats playerStats)
    {
        if (isFood)
        {
            playerStats.IncreaseHunger(hungerRestoreAmount);
        }

        if (isWater)
        {
            playerStats.IncreaseThirst(thirstRestoreAmount);
        }

        if (isHealth)
        {
            playerStats.IncreaseHealth(healthRestoreAmount);
        }

        Destroy(gameObject);
    }
}
