using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacmentSnap : MonoBehaviour
{
    [SerializeField] private GameObject correctObject; // The object that should go here
    private bool isCorrectlyPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == correctObject)
        {
            isCorrectlyPlaced = true;
            Debug.Log("Correct object placed!");
            // Add feedback logic here, like sound or animation
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == correctObject)
        {
            isCorrectlyPlaced = false;
            Debug.Log("Object removed from correct spot.");
        }
    }
}
