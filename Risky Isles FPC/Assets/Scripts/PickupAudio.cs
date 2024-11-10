using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float playDuration = 5f;

    private Coroutine playCoroutine;

    public void PlayAudio()
    {
        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
        }
        playCoroutine = StartCoroutine(PlayAudioForDuration());
    }

    private IEnumerator PlayAudioForDuration()
    {
        audioSource.Play();
        yield return new WaitForSeconds(playDuration);
        audioSource.Stop();
    }
}
