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
    public float delayBeforeDeath = 10f;
    
    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player triggered the shelf!");
            isTriggered = true;
            StartCoroutine(RotateShelf());
            StartCoroutine(DeathAnimation());
            
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

    public IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(delayBeforeDeath);
        FirstPersonControls.Instance.DeathAnim();
    }

}