using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuActivator : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject startMenu;
    public Vector3 targetPosition;
    public Vector3 targetRotation;
    public float positionThreshold = 0.1f;
    public float rotationThreshold = 5f;

    void Update()
    {
        bool isPositionCorrect = Vector3.Distance(cameraTransform.position, targetPosition) <= positionThreshold;
        bool isRotationCorrect = Quaternion.Angle(cameraTransform.rotation, Quaternion.Euler(targetRotation)) <=
                                 rotationThreshold;
        if (isPositionCorrect && isRotationCorrect)
        {
            startMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(false);
        }
    }
}
