using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera newCamera;
    [SerializeField] private GameObject buttonObject;

    private bool isColliderEntered = false;
    private bool isNewCameraActive = false;

    private void OnTriggerEnter(Collider other)
    {
        // Enable camera switching when player enters collider
        isColliderEntered = true;

        // Show button
        if (buttonObject != null)
        {
            buttonObject.SetActive(true);
        }

        Debug.Log("Entered collider - Press camera button to switch camera");
    }

    private void OnTriggerExit(Collider other)
    {
        // Disable camera switching when player exits collider
        isColliderEntered = false;

        // Hide button
        if (buttonObject != null)
        {
            buttonObject.SetActive(false);
        }

        // Reset camera to inactive if new camera was active
        if (isNewCameraActive)
        {
            SwitchCamera();
        }

        Debug.Log("Exited collider");
    }

    public void OnCameraButtonPressed()
    {
        // Only allow camera switching if collider is entered
        if (isColliderEntered)
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (isNewCameraActive)
        {
            // Deactivate new camera
            newCamera.enabled = false;
            isNewCameraActive = false;
            Debug.Log("Camera deactivated");
        }
        else
        {
            // Activate new camera
            newCamera.enabled = true;
            isNewCameraActive = true;
            Debug.Log("Camera activated");
        }
    }
}