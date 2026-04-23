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
    [SerializeField] private bool in_Dialogue = false;
    [SerializeField] private bool ending_Dialogue = false;
    [SerializeField] private bool mid_Pan = false;
    
    
    
    void Update()
    {
        if (in_Dialogue == true)
        {
            // If entering dialogue
            if (ending_Dialogue == false && mid_Pan == true)
            {
                transform.position = Vector3.Lerp(transform.position, mid_Dialogue_Position, Time.deltaTime * pan_Speed);
                transform.LookAt(mid_Dialogue_Rotation);
                
                if (transform.position == mid_Dialogue_Position)
                    mid_Pan = false;
            }
            
            // If leaving dialogue
            else if (ending_Dialogue == true && mid_Pan == true)
            {
                transform.position = Vector3.Lerp(transform.position, playerPos.position + new Vector3(1, 2, -10), Time.deltaTime * pan_Speed);
                transform.rotation = Quaternion.Lerp(transform.rotation, pre_Dialogue_Rotation, Time.deltaTime * pan_Speed);
                
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
        
        pre_Dialogue_Position = transform.position;
        mid_Dialogue_Position = camera_Position_Target;
        
        pre_Dialogue_Rotation = this.transform.rotation;
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
