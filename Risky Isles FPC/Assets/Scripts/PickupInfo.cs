using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupInfo : MonoBehaviour
{
    public Sprite infoDisplay;
    [TextArea] public string pickupMessage = "A mysterious object";

    public void DisplayInformation(Image displayImage, TextMeshProUGUI information)
    {
        if (infoDisplay != null)
        {
            displayImage.sprite = infoDisplay;
            information.text = pickupMessage;
        }
    }

    public void StopShowingInformation(TextMeshProUGUI information)
    {
        information.text = null;
    }
}
