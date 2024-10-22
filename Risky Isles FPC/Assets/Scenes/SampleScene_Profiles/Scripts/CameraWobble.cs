using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public float intensity = 0.1f;
    public float speed = 5f;

    private Vector3 originalPosition; 
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    
    void Update()
    {
        float wobbleX = Mathf.Sin(Time.time * speed) * intensity;
        float wobbleY = Mathf.Cos(Time.time * speed) * intensity;

        transform.localPosition = originalPosition + new Vector3(wobbleX, wobbleY, 0);
    }
}
