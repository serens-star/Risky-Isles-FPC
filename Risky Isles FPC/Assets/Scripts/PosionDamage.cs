using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionDamage : MonoBehaviour
{
    public FirstPersonControls playerTarget;
    public int DamageDealtOverTime;
    public float timeBetweenDamage;

    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(DMGoverTime());
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(DMGoverTime());
    }

    public IEnumerator DMGoverTime()
    {
        yield return new WaitForSeconds(timeBetweenDamage);
        playerTarget.playerHp -= DamageDealtOverTime;        
    }
}
