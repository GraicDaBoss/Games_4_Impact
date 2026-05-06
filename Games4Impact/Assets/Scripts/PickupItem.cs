using System;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Pickup(Transform headPoint)
    {
        rb.isKinematic = true;
        col.enabled = false;
        transform.SetParent(headPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void PutDown(Vector3 dropPosition)
    {
        rb.isKinematic = false;
        col.enabled = true;
        transform.SetParent(null);
        transform.position = dropPosition;
    }
}
