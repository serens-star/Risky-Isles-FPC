using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public AudioSource InteractionAudio;
    public LayerMask interactableLayer;
    public float interactionRange = 3f;
    public Transform playerCamera;

    private bool isLookingAtObject = false;

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (InteractionAudio != null)
                {
                    InteractionAudio.Play();
                }
                else
                {
                    Debug.LogWarning("No Voice");
                }
            }
        }
    }
}