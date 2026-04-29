using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camerafollow : MonoBehaviour
{
    public Transform playerPos;

    // For panning in and out of dialogue
    [Header("Dialogue Controls")] 
    [SerializeField] private float pan_Speed;
    [SerializeField] private float pan_Allowance; // Lerp takes too long to be accurate
    
    [SerializeField] private Vector3 pre_Dialogue_Position;
    [SerializeField] private Vector3 mid_Dialogue_Position;
    
    private Quaternion pre_Dialogue_Rotation;
    private Vector3 mid_Dialogue_Rotation;

    [Header("Exposed for testing - Do not change")]
    public bool in_Dialogue = false;
    public bool ending_Dialogue = false;
    public bool mid_Pan = false;


    private void Start()
    {
        pre_Dialogue_Rotation = this.transform.rotation;

    }

    void Update()
    {
        if (in_Dialogue == true)
        {
            // If entering dialogue
            if (ending_Dialogue == false && mid_Pan == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, mid_Dialogue_Position, Time.deltaTime * pan_Speed);
                transform.LookAt(mid_Dialogue_Rotation);
                
                if (transform.position == mid_Dialogue_Position)
                    mid_Pan = false;
            }
            
            // If leaving dialogue
            else if (ending_Dialogue == true && mid_Pan == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position + new Vector3(1, 2, -10), Time.deltaTime * pan_Speed);
                this.transform.LookAt(playerPos.position);
                
                if (Vector3.Distance(transform.position, playerPos.position + new Vector3(1, 2, -10)) <= pan_Allowance)
                    Reset_Values();
            }

        }
        
        else
            transform.position = playerPos.position + new Vector3(1, 2, -10); 
    }


    public void Pan_To_Dialogue(Vector3 camera_Position_Target, Vector3 camera_Rotation_Target)
    {
        in_Dialogue = true;

        // Check if in dialogue to not overwrite on mid dialogue pan
        if (in_Dialogue == false)
        {
            pre_Dialogue_Position = transform.position;
        }
        
        mid_Dialogue_Position = camera_Position_Target;
        mid_Dialogue_Rotation = camera_Rotation_Target;
        
        mid_Pan = true;
    }

    public void Pan_From_Dialogue()
    {
        ending_Dialogue = true;
        mid_Pan = true;
    }

    private void Reset_Values()
    {
        in_Dialogue = false;
        mid_Pan = false;
        ending_Dialogue = false;
    }
    
    
}
