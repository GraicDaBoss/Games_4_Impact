using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue_System : MonoBehaviour
{
    [Header("Dialogue Functionality")]
    [SerializeField] private Character_Script character_Script;
    [SerializeField] private string[] current_Dialogue;
    public int current_Dialogue_I = 0; // Dialogue ID
    private int current_Line_I = 0; // Individual line within Dialogue ID
    private bool currently_Typing = false; // For checking if dialogue is actively being written
    
    [Header("UI Functionality")]
    [SerializeField] private GameObject dialogue_Box; // To hide/unhide whole dialogue box
    [SerializeField] private TextMeshProUGUI text_Component;
    [SerializeField] private float text_Speed;
    //[SerializeField] private AudioSource talking_Clip;

    [Header("Restrict Player Controls")]
    // figure out later what needs to be turned off where
    // will probably have to use bools to return; specific functions instead of turning scripts off 
    [SerializeField] private PlayerWalk player_Controls;
    [SerializeField] private On_Interact interact_Script;
    public bool in_Dialogue = false;
    
    [Header("Restrict Camera Controls")]
    [SerializeField] private GameObject dialogue_Camera_Position; // Object under NPC prefab to place camera angle
    private camerafollow camera_Script;

    

    private void Start()
    {
        camera_Script = Camera.main.GetComponent<camerafollow>();
    }


    public void Start_Dialogue()
    {
        // Retrieve dialogue
        current_Dialogue = character_Script.Get_Dialogue(current_Dialogue_I);
        if (current_Dialogue == null)
        {
            print("ERROR: Dialogue not found");
            return;
        }
        
        interact_Script.npc_Can_Interact = true;
        
        // Set up camera
        Vector3 camera_Target_Position = dialogue_Camera_Position.transform.position;
        
        camera_Script.Pan_To_Dialogue(camera_Target_Position, this.transform.position);
        
        // Start writing dialogue
        Toggle_Dialogue();
        StartCoroutine(Type_Line());
    }
    
    
    IEnumerator Type_Line()
    {
        text_Component.text = "";
        
        //talking_Clip.Play();
        currently_Typing = true;
        // Add each character in the line incrementally
        foreach (char c in current_Dialogue[current_Line_I].ToCharArray())
        {
            text_Component.text += c;
            yield return new WaitForSeconds(text_Speed);
        }
        currently_Typing = false;
        current_Line_I++;
        //talking_Clip.Pause();
        
    }

    
    // When interact is pressed mid dialogue
    public void Dialogue_Interacted()
    {
        // If typing, finish line
        if (currently_Typing == true)
        {
            StopAllCoroutines();
            //talking_Clip.Pause();
            text_Component.text = current_Dialogue[current_Line_I];
            currently_Typing = false;
            current_Line_I++;
        }

        // If we are past the amount of total lines in the dialogue, end it
        else if (current_Line_I >= current_Dialogue.Length)
        {
            // Set to reinteract line, even if there has been other dialogue
            if (current_Dialogue_I != 1)
                current_Dialogue_I = 1;
            
            camera_Script.Pan_From_Dialogue();
            Toggle_Dialogue();
            interact_Script.npc_Can_Interact = false;
        }
            
        // Else move onto next line
        else
        {
            StartCoroutine(Type_Line());
        }
    }
    
    // For toggling UI, bools, indexes
    private void Toggle_Dialogue()
    {
        dialogue_Box.SetActive(!dialogue_Box.activeSelf);
        text_Component.text = "";
        in_Dialogue = !in_Dialogue;
        player_Controls.in_Dialogue = in_Dialogue;
        current_Line_I = 0;
    }
    
    
}// end script
