using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider HealthBar;
    [SerializeField] private float Health = 100;
    [SerializeField]private bool Choking;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone"))
        {
            Choking = true;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PoisonZone"))
        {
            Choking = false;
        }
        
    }
   


    public void Update()
    {
        HealthBar.value = Health;
        if (Choking)
        {
            Health -= 0.02f;
        }
    }

   
}
