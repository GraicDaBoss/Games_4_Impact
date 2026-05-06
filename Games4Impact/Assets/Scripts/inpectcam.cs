using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera newCamera;
    [SerializeField] private GameObject buttonObject;
    
    private bool isColliderEntered = false;
    private bool isNewCameraActive = false;

    private void OnTriggerEnter(Collider other)
    {
        
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
        
        isColliderEntered = false;

        // Hide button
        if (buttonObject != null)
        {
            buttonObject.SetActive(false);
        }

        
        if (isNewCameraActive)
        {
            SwitchCamera();
        }
        
        Debug.Log("Exited collider");
    }

    public void OnCameraButtonPressed()
    {
       
        if (isColliderEntered)
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (isNewCameraActive)
        {
           
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