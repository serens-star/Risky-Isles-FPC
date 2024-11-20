using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupInfo : MonoBehaviour
{
    public Sprite infoDisplay;
    public string pickupMessage = "A mysterious object";

    public void DisplayInformation(Image displayImage, TextMeshProUGUI information)
    {
        displayImage.gameObject.SetActive(true);
        displayImage.sprite = infoDisplay;
        information.text = pickupMessage;
    }

    public void StopShowingInformation(Image displayImage)
    {
        displayImage.gameObject.SetActive(false);
    }
}
