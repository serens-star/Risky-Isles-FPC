using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            // Calculate the direction of the force
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0; // Ignore vertical force
            forceDirection.Normalize(); // Ensure the force direction is a unit vector

            // Apply the force at the point of collision
            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, hit.point, ForceMode.Impulse);
        }
    }
}