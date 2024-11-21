using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShelfTrigger : MonoBehaviour
{
    [Header("Shelf Settings")]
    public GameObject shelf; 
    public float rotationSpeed = 2f; 
    private bool isTriggered = false;

    [Header("Kill Player")] 
    public CanvasGroup fadeImage;

    public float fadeDuration;
    public float delayBeforeFade;
    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player triggered the shelf!");
            isTriggered = true;
            StartCoroutine(PlayDeath());
            StartCoroutine(FadeToBlackAfterDelay());            
            StartCoroutine(RotateShelf());

        }
    }

    private IEnumerator RotateShelf()
    {
        Quaternion initialRotation = shelf.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, -180f, 0f);

        float timeElapsed = 0f;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rotationSpeed;
            shelf.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed);
            yield return null; 
        }
        shelf.transform.rotation = targetRotation;
        yield return null;
    }

    private IEnumerator PlayDeath()
    {
        yield return new WaitForSeconds(delayBeforeFade - 5f);
        FirstPersonControls.Instance.DeathAnim();
    }
    private IEnumerator FadeToBlackAfterDelay()
    {
        //isPlayerInTrigger = true;

        //yield return new WaitForSeconds(delayBeforeFade);
        //FirstPersonControls.Instance.DeathAnim();

        yield return new WaitForSeconds(delayBeforeFade);
        /*if (isPlayerInTrigger)
        {*/
        yield return null;
        fadeImage.alpha = 0;
        float elapsed = 0f;
        yield return null;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.alpha = Mathf.Lerp(fadeImage.alpha, 1f, elapsed / fadeDuration);

            yield return null;
        }

        fadeImage.alpha = 1;
        yield return null;
            //}

    }

    private void ResetFade()
    {

    }
}