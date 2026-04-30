using System;
using UnityEngine;

public class Character_Script : MonoBehaviour
{
    [Header("Dialogue Text - Elements are different lines")] // Different types of dialogue, arrays contain individual lines
    [SerializeField] private string[] first_Dialogue;
    [SerializeField] private string[] reinteract_Dialogue;
    [SerializeField] private string[] player_Correct_Dialogue;
    [SerializeField] private string[] player_Wrong_Dialogue;
    [SerializeField] private string[] finished_Dialogue;
    
    
    // Call this to bring string into Dialogue_System for printing
    public (string[], bool) Get_Dialogue(int dialogue_ID)
    {
        if (dialogue_ID == 0)
            return (first_Dialogue, true);

        if (dialogue_ID == 1)
            return (reinteract_Dialogue, false);
        
        if (dialogue_ID == 2)
            return (player_Correct_Dialogue, false);
        
        if (dialogue_ID == 3)
            return (player_Wrong_Dialogue, false);
        
        if (dialogue_ID == 4)
            return (finished_Dialogue, false);
        
        else
            return (null, false);

    }
    
}