using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    public Light flickerLight;
    public float minWait;
    public float maxWait;
    void Awake()
    {
        StartCoroutine(LightFlicker());
    }
    

    public IEnumerator LightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            flickerLight.enabled = !flickerLight.enabled;
        }
    }
    
}
