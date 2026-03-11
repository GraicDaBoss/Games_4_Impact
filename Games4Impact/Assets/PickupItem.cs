using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public void Pickup(Transform headPoint)
    {
        transform.SetParent(headPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void PutDown(Vector3 dropPosition)
    {
        transform.SetParent(null);
        transform.position = dropPosition;
    }
}
