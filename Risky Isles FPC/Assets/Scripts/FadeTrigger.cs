/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 7f;
    public float delayBeforeFade = 4.5f;

    private Coroutine fadeCoroutine;
    private bool isPlayerInTrigger = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeToBlackAfterDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
            isPlayerInTrigger = false;
            ResetFade();
        }
    }

    private IEnumerator FadeToBlackAfterDelay()
    {
        isPlayerInTrigger = true;

        yield return new WaitForSeconds(delayBeforeFade);

        if (isPlayerInTrigger)
        {
            float elapsed = 0f;
            Color color = fadeImage.color;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }

            color.a = 1f;
            fadeImage.color = color;
        }

        fadeCoroutine = null;
    }

    private void ResetFade()
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }
}*/
