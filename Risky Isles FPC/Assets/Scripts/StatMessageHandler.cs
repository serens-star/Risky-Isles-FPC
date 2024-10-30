using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartMessageHandler : MonoBehaviour
{
    public GameObject startMessageUI; // Assign your Text UI element
    public float displayDuration = 9f; // Duration to display the message

    void Start()
    {
        // Activate the start message UI at the beginning
        if (startMessageUI != null)
        {
            startMessageUI.SetActive(true);
            // Call a function to hide the message after a delay
            Invoke("HideStartMessage", displayDuration);
        }
    }

    void HideStartMessage()
    {
        // Disable the UI element after the delay
        if (startMessageUI != null)
        {
            startMessageUI.SetActive(false);
        }
    }
}
