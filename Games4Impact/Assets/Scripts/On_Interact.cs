
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class On_Interact : MonoBehaviour
{
    private PickupItem currentItem;
    private PickupItem carriedItem;

    public Transform headPoint;

    [Header("Dialogue")] 
    public bool npc_Can_Interact;
    [SerializeField] private GameObject NPC = null;
    public bool is_Puzzle_NPC = false;
    
    [Header("Sort Buttons")]
    private Sort_Zone sort_Button_Script;
    private bool can_Sort = false;

    [Header("Inspect Button")]
    private CameraSwitcher inspect_Button_Script = null;
    public bool can_Inspect = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            npc_Can_Interact = true;
            NPC = other.gameObject;
        }
        
        else if (other.CompareTag("Buttons"))
        {
            sort_Button_Script = other.GetComponent<Sort_Zone>();
            can_Sort = true;
        }

        else if (other.CompareTag("Inspect Button"))
        {
            inspect_Button_Script = other.GetComponent<CameraSwitcher>();
            can_Inspect = true;
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
            if (is_Puzzle_NPC) // Stops data loss for puzzle, otherwise can't interact to leave dialogue
                return;
            
            npc_Can_Interact = false;
            NPC = null;
        }
        
        else if (other.CompareTag("Buttons"))
        {
            sort_Button_Script = null;
            can_Sort = false;
        }
        
        else if (other.CompareTag("Inspect Button"))
        {
            inspect_Button_Script = null;
            can_Inspect = false;
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
            {
                // Skip if Puzzle NPC since interations are triggered differently
                if (is_Puzzle_NPC)
                    return;
                
                NPC.GetComponent<Dialogue_System>().Start_Dialogue();
            }
        }
            
        else if (can_Sort == true)
        {
            if (sort_Button_Script == null)
                return;
            
            sort_Button_Script.OnInteractPressed();
        }
        
        else if (can_Inspect == true)
        {
            if (inspect_Button_Script == null)
                return;
            
            inspect_Button_Script.OnCameraButtonPressed();
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

    // Puzzle NPC starts through a trigger not interacting, this links the NPC properly
    // Otherwise, cant interact with dialogue, or even leave it
    public void Link_Puzzle_NPC()
    {
        is_Puzzle_NPC = true;
        NPC = GameObject.FindGameObjectWithTag("NPC");
    }
    
}

