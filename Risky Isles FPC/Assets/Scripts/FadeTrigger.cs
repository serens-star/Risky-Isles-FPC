using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeTrigger : MonoBehaviour
{
    [Header("Kill Player")] 
    public CanvasGroup fadeImage;
    public float fadeDuration = 7f;
    public Animator fadeAnim;

    public void FadeScreen()
    {
        fadeImage.gameObject.SetActive(true);        
        fadeImage.alpha = 0;        
        StartCoroutine(FadeToBlack());
    }

    public void ActivateFade()
    {
        fadeAnim.gameObject.SetActive(true);
    }

    public IEnumerator FadeToBlack()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.alpha = Mathf.Lerp(fadeImage.alpha, 1f, timer/fadeDuration);
            yield return null;
        }

        fadeImage.alpha = 1;
        yield return null;
    }
}
