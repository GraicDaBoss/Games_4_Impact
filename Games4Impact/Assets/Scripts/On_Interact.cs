
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class On_Interact : MonoBehaviour
{
    private PickupItem currentItem;
    private PickupItem carriedItem;

    public Transform headPoint;

    private void OnTriggerEnter(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null)
        {
            currentItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null && item == currentItem)
        {
            currentItem = null;
        }
    }

    public void Interact()
    {
        // Put down 
        if (carriedItem != null)
        {
            carriedItem.PutDown(transform.position + transform.forward * 1.5f);
            carriedItem = null;
            return;
        }

        // Pick up
        if (currentItem != null)
        {
            currentItem.Pickup(headPoint);
            carriedItem = currentItem;
            currentItem = null;
        }
    }
}