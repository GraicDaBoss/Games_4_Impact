using UnityEngine;

public class inspectshirt : MonoBehaviour
{
    public Camera targetCamera;
    public Transform targetPosition;
    public float moveSpeed = 5f;

    
    public float newFOV = 30f;
    public float speed = 5f;

    Vector3 originalPosition;
    Quaternion originalRotation;
    float originalFOV;

    bool triggered;

    void Start()
    {
        originalPosition = targetCamera.transform.position;
        originalRotation = targetCamera.transform.rotation;
        originalFOV = targetCamera.fieldOfView;
    }

    public void OnInteract()
    {
        triggered = !triggered; 
    }

    void Update()
    {
        if (triggered)
        {
            
            targetCamera.transform.position = Vector3.Lerp(
                targetCamera.transform.position,
                targetPosition.position,
                Time.deltaTime * moveSpeed
            );

            targetCamera.transform.rotation = Quaternion.Lerp(
                targetCamera.transform.rotation,
                targetPosition.rotation,
                Time.deltaTime * moveSpeed
            );

            
            targetCamera.fieldOfView = Mathf.Lerp(
                targetCamera.fieldOfView,
                newFOV,
                Time.deltaTime * speed
            );
        }
        else
        {
            
            targetCamera.transform.position = Vector3.Lerp(
                targetCamera.transform.position,
                originalPosition,
                Time.deltaTime * moveSpeed
            );

            targetCamera.transform.rotation = Quaternion.Lerp(
                targetCamera.transform.rotation,
                originalRotation,
                Time.deltaTime * moveSpeed
            );

            
            targetCamera.fieldOfView = Mathf.Lerp(
                targetCamera.fieldOfView,
                originalFOV,
                Time.deltaTime * speed
            );
        }
    }
}
