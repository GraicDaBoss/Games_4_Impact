using UnityEngine;

public class ChangeFOVOnTrigger : MonoBehaviour
{
    public Camera targetCamera;
    public float newFOV;
    public float speed = 5f;
    public GameObject targetObject;

    bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            triggered = true;
        }
    }

    private void Update()
    {
        if (triggered)
        {
            targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, newFOV, Time.deltaTime * speed);
        }
    }
}