using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public bool isFood;
    public bool isWater;
    public int hungerRestoreAmount;
    public int thirstRestoreAmount;

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

        Destroy(gameObject);
    }
}
