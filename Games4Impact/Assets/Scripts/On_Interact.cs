
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class On_Interact : MonoBehaviour
{
    private PickupItem currentItem;
    private PickupItem carriedItem;

    public Transform headPoint;

    [Header("Dialogue")] 
    [SerializeField] private bool npc_Can_Interact;
    [SerializeField] private GameObject NPC = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npc_Can_Interact = true;
            NPC = other.gameObject;
        }
        
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null)
        {
            currentItem = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npc_Can_Interact = false;
            NPC = null;
        }
        
        PickupItem item = other.GetComponent<PickupItem>();
        if (item != null && item == currentItem)
        {
            currentItem = null;
        }
    }

    public void Interact()
    {
        // Prioritise interacting with NPC
        if (npc_Can_Interact == true)
        {
            if (NPC == null)
            {
                print("ERROR: NPC not detected properly");
                return;
            }

            // Check if we're already in a dialogue, for skipping type out or going to next line
            if (NPC.GetComponent<Dialogue_System>().in_Dialogue == true)
            {
                NPC.GetComponent<Dialogue_System>().Dialogue_Interacted();
            }
            
            // otherwise start the dialogue
            else
                NPC.GetComponent<Dialogue_System>().Start_Dialogue();
        }
            
        // Interact with items instead
        else
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
}