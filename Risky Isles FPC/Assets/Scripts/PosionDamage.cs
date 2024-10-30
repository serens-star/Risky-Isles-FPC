using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PosionDamage : MonoBehaviour
{
    public GameObject player;
    public int DamageDealtOverTime = 5;
    public float timeBetweenDamage = 2f;

    private PlayerStats playerStats;
    private bool isDamaging = false;

    private void Start()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isDamaging)
        {
            isDamaging = true;
            StartCoroutine(DMGoverTime());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDamaging = false;
            StopCoroutine(DMGoverTime());
        }
    }

    public IEnumerator DMGoverTime()
    {
        while (isDamaging)
        {
            yield return new WaitForSeconds(timeBetweenDamage);
            if (playerStats != null)
            {
                playerStats.TakeDamage(DamageDealtOverTime);
            }
        }

        isDamaging = false;
    }
}*/
