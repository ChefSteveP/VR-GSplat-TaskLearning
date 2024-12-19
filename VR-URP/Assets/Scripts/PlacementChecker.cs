using Meta.XR.MRUtilityKit.SceneDecorator;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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
    private Color originalColor;
    private Renderer rend;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();
        rend = templateObject.GetComponent<Renderer>();
        originalColor = rend.material.color;

        if (grabInteractable != null)
        {
            // Subscribe to grab events
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
        }
    }
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed.");
        if(!isPlacedCorrectly){
            templateObject.gameObject.SetActive(true);
        }
        
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Check if the release event was canceled
        if (args.isCanceled)
        {
            Debug.Log("Release was canceled. Skipping success check.");
            return;
        }

        Debug.Log("Object released.");

        // Perform success check only if the release is valid
        if (IsPlacedCorrectly())
        {
            Debug.Log("Teapot placed correctly!");
            TriggerSuccessAction();
        }
        else
        {
            Debug.Log("Teapot not in the correct position/rotation.");
            templateObject.gameObject.SetActive(false);
        }
    }   
    void Update()
    {
        if (IsPlacedCorrectly() && templateObject.gameObject.activeSelf)
        {
            // change color of templateObject to green
            rend.material.color = Color.green;
        }
        else {
            //keep original color
            rend.material.color = originalColor;
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
        isPlacedCorrectly = true;

        Debug.Log("Success effect triggered!");
    }

    private void OnDestroy()
    {
        // Cleanup listeners when the object is destroyed
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}
