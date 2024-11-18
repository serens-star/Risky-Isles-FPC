using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLighting : MonoBehaviour
{
    public Light[] roomLights;
    public float dimIntensity = 1111f;
    public float brightIntensity = 2222f;
    public float transitionSpeed = 1f;

    private bool isDimming = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDimming = true;
        }
    }

    void Update()
    {
        if (isDimming)
        {
            foreach (Light light in roomLights)
            {
                light.intensity = Mathf.Lerp(light.intensity, dimIntensity, Time.deltaTime * transitionSpeed);
            }
        }
        else
        {
            foreach (Light light in roomLights)
            {
                light.intensity = Mathf.Lerp(light.intensity, brightIntensity, Time.deltaTime * transitionSpeed);
            }
        }
    }
}
