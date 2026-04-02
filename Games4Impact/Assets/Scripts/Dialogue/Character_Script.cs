using System;
using UnityEngine;

public class Character_Script : MonoBehaviour
{
    [Header("Dialogue Text - Elements are different lines")] // Different types of dialogue, arrays contain individual lines
    [SerializeField] private string[] first_Dialogue;
    [SerializeField] private string[] reinteract_Dialogue;
    
    
    // Call this to bring string into Dialogue_System for printing
    public string[] Get_Dialogue(int dialogue_ID)
    {
        if (dialogue_ID == 0)
            return first_Dialogue;

        if (dialogue_ID == 1)
            return reinteract_Dialogue;
        
        else
            return null;

    }
    
}
