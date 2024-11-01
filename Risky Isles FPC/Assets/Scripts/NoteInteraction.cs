using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this line to include the UI namespace

/*public class NoteInteraction : MonoBehaviour
{
    public GameObject noteTextUI; // Reference to the UI text component
    public string noteContent;
    public float interactionDistance = 3f;

    private Transform player;
    private bool isReading = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        noteTextUI.SetActive(false);
        
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (noteTextUI == null)
            {
                Debug.LogError("NoteTextUI is not assigned in the Inspector!");
            }

            if (player == null)
            {
                Debug.LogError("Player object not found! Make sure the player is tagged 'Player'.");
            }

            noteTextUI.SetActive(false);
        

    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
    
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E) && !isReading)
        {
            ShowNote();
        }
        else if (isReading && Input.GetKeyDown(KeyCode.Escape))
        {
            HideNote();
        }
    }


    void ShowNote()
    {
        noteTextUI.SetActive(true);
        noteTextUI.GetComponent<Text>().text = noteContent;  // Using Text component from UnityEngine.UI
        isReading = true;
    }

    void HideNote()
    {
        noteTextUI.SetActive(false);
        isReading = false;
    }
}*/
