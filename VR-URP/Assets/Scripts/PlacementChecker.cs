using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit.XRGrabInteracable;

public class PlacementChecker : MonoBehaviour
{
    [Header("Template Object")]
    public Transform templateObject; // Assign the blue teapot transform here in the inspector.

    [Header("Thresholds")]
    public float positionThreshold = 0.1f; // Allowed position error (in meters)
    public float rotationThreshold = 5f;   // Allowed rotation error (in degrees)

    [Header("Feedback")]
    public GameObject successEffect; // Optional: Assign a particle effect or visual feedback

    private bool isPlacedCorrectly = false;

    // private XRGrabInteractable grabInteractable;
    // private bool isHolding = false; // Tracks whether the object is being held


    void Update()
    {
        if (IsPlacedCorrectly() && !isPlacedCorrectly)
        {
            Debug.Log("Teapot placed correctly!");
            TriggerSuccessAction();
            isPlacedCorrectly = true; // Prevent multiple triggers
        }
        else if (!IsPlacedCorrectly() && isPlacedCorrectly)
        {
            // Reset if the object is moved again
            isPlacedCorrectly = false;
        }
    }

    private bool IsPlacedCorrectly()
    {
        // Check position
        float positionDistance = Vector3.Distance(transform.position, templateObject.position);
        if (positionDistance > positionThreshold)
            return false;

        // Check rotation
        float rotationDifference = Quaternion.Angle(transform.rotation, templateObject.rotation);
        if (rotationDifference > rotationThreshold)
            return false;

        return true;
    }

    private void TriggerSuccessAction()
    {
        successEffect.SetActive(true);
        templateObject.gameObject.SetActive(false);

        Debug.Log("Success effect triggered!");
    }
}
