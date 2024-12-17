using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShatter : MonoBehaviour
{
    public GameObject shatteredObjectPrefab;    // Reference to the shattered Object Prefab (set in Inspector)
    public float forceThreshold = 10f;          // Threshold force to trigger the swap

    void OnCollisionEnter(Collision collision)
    {
        // Get the force applied in the collision
        float impactForce = collision.relativeVelocity.magnitude;

        // Check if the force exceeds the threshold
        if (impactForce > forceThreshold)
        {
            SwapObject();
        }
    }

    void SwapObject()
    {
        // Instantiate the shattered object at the same position and rotation as the solid object
        GameObject shatteredObject = Instantiate(shatteredObjectPrefab, transform.position, transform.rotation * Quaternion.Euler(90f, 0f, 0f));

        // // Enable physics on the shattered object
        // Rigidbody shatteredRigidbody = shatteredObject.GetComponent<Rigidbody>();
        // shatteredRigidbody.isKinematic = false;

        // Destroy the original solid object
        Destroy(gameObject);
    }
}
