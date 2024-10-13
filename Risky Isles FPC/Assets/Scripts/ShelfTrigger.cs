using UnityEngine;

public class ShelfTrigger : MonoBehaviour
{
    [Header("Shelf Settings")]
    public GameObject shelf; 
    public float rotationSpeed = 2f; 

    private bool isTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") && !isTriggered)
        {
            Debug.Log("Player triggered the shelf!");
            isTriggered = true;
            StartCoroutine(RotateShelf());
        }
    }

    private System.Collections.IEnumerator RotateShelf()
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
}