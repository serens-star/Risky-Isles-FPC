using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AreaAudioSequencer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float[] clipDurations;
    public Collider triggerArea;

    private AudioSource audioSource;
    private bool isPlayerInArea = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (clipDurations.Length != audioClips.Length)
        {
            Debug.LogError("Clip durations & audio clips arrays have the same length");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            PlayClipsInSequence();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            audioSource.Stop();
            StopAllCoroutines();
        }
    }

    private void PlayClipsInSequence()
    {
        if (audioClips.Length == 0 || clipDurations.Length == 0) return;
        StartCoroutine(PlayClips());
    }

    private IEnumerator PlayClips()
    {
        double startTime = AudioSettings.dspTime;
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (!isPlayerInArea) yield break;

            audioSource.clip = audioClips[i];
            audioSource.PlayScheduled(startTime);
            startTime += clipDurations[i];

            yield return new WaitForSeconds((float)(startTime - AudioSettings.dspTime));
        }
    }
}
