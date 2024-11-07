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

            normalColor = material.GetColor("_EmissionColor");
            originalEmissionIntensity = Mathf.Max(normalColor.r, normalColor.g, normalColor.b);
        }
        else
        {
            Debug.LogError("Renderer component not found on " + gameObject.name);
        }
        
    }
    
    void Update()
    {
        if (material == null) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distance);

        if (distance > glistenDistance)
        {
            float intensity = Mathf.Lerp(0, maxEmissionIntensity, (distance - glistenDistance) / glistenDistance);
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
