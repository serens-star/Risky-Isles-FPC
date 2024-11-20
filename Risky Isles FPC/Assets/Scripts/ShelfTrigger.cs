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
    public Image fadeImage;

    public float fadeDuration;
    public float delayBeforeFade;
    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player triggered the shelf!");
            isTriggered = true;
            StartCoroutine(RotateShelf());
            StartCoroutine(FadeToBlackAfterDelay());
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
    
    private IEnumerator FadeToBlackAfterDelay()
    {
        //isPlayerInTrigger = true;

        yield return new WaitForSeconds(delayBeforeFade);
        FirstPersonControls.Instance.DeathAnim();

        yield return new WaitForSeconds(3f);
        /*if (isPlayerInTrigger)
        {*/
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
            yield return null;
            //}

    }

    private void ResetFade()
    {
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }
}