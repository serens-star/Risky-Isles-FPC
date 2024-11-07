using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlistenEffect : MonoBehaviour
{
    public Transform player;
    public float glistenDistance = 500f;
    public Color glistenColor = Color.yellow;
    public float maxEmissionIntensity = 300f;

    private Material material;
    private Color normalColor;
    private float originalEmissionIntensity;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            if (material.HasProperty("_EmissionColor"))
            {
                normalColor = material.GetColor("_EmissionColor");
                originalEmissionIntensity = Mathf.Max(normalColor.r, normalColor.g, normalColor.b);
            }
            else
            {
                Debug.LogError("Material doesn't have an _EmissionColor property");
            }

        }
        else
        {
            Debug.LogError("Renderer component not found on " + gameObject.name);
        }
        
    }
    
    void Update()
    {
        if (material == null || !material.HasProperty("_EmissionColor")) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distance);

        if (distance > glistenDistance)
        {
            float intensityFactor = Mathf.Clamp01((distance - glistenDistance) / glistenDistance);
            float intensity = Mathf.Pow(intensityFactor, 2) * maxEmissionIntensity;
            
            Color emissionColor = glistenColor * intensity;
            Debug.Log("Applying emission color: " + emissionColor);
            
            material.SetColor("_EmissionColor", emissionColor);
            material.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.Log("Resetting to normal color: " + normalColor);
            material.SetColor("_EmissionColor", normalColor);
            material.DisableKeyword("_EMISSION");
        }
    }
}
