using UnityEngine;

public class inspectshirt : MonoBehaviour
{
    public Camera targetCamera;
    public Transform targetPosition;
    public float moveSpeed = 5f;

    Vector3 originalPosition;
    Quaternion originalRotation;
    bool moved;

    void Start()
    {
        originalPosition = targetCamera.transform.position;
        originalRotation = targetCamera.transform.rotation;
    }

    public void OnInteract()
    {
        moved = !moved;
    }

    void Update()
    {
        if (moved)
        {
            targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, targetPosition.position, Time.deltaTime * moveSpeed);
            targetCamera.transform.rotation = Quaternion.Lerp(targetCamera.transform.rotation, targetPosition.rotation, Time.deltaTime * moveSpeed);
        }
        else
        {
            targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, originalPosition, Time.deltaTime * moveSpeed);
            targetCamera.transform.rotation = Quaternion.Lerp(targetCamera.transform.rotation, originalRotation, Time.deltaTime * moveSpeed);
        }
    }
}