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
    public float fadeDuration = 7f;
    public float delayBeforeFade = 10f;

   
    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player triggered the shelf!");
            isTriggered = true;
            StartCoroutine(RotateShelf());
            StartCoroutine(HandleDeathSequence());
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
    }
    private IEnumerator HandleDeathSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);
        
        FirstPersonControls.Instance.DeathAnim();
        Debug.Log("Playing Death Animation");

        yield return new WaitForSeconds(delayBeforeFade);
        
        Debug.Log("Starting Fade to Black");
        fadeImage.alpha = 0;
        float elapsed = 0f;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.alpha = Mathf.Lerp(0,1,elapsed / fadeDuration);
            yield return null;
        }

        fadeImage.alpha = 1;
        Debug.Log("Fade to black completed");
        
        //yield return StartCoroutine(PlayDeath());
        //yield return StartCoroutine(FadeToBlackAfterDelay());
    }

    /*private IEnumerator PlayDeath()
    {
        Debug.Log($"Waiting {delayBeforeFade} seconds before triggering the death animation");
        yield return new WaitForSeconds(delayBeforeFade);
        
    }*/
    /*private IEnumerator FadeToBlackAfterDelay()
    {
        //isPlayerInTrigger = true;

        //yield return new WaitForSeconds(delayBeforeFade);
        //
        Debug.Log($"Waiting {delayBeforeFade} seconds before triggering the death animation");
        yield return new WaitForSeconds(delayBeforeFade);
        
        
        /*if (isPlayerInTrigger)
        {*/
        //yield return null;
        
        //yield return null;
       
       
        //yield return null;
            //}
            //}

    /*private void ResetFade()
    {

    }*/
}