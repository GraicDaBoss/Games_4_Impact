using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private Dialogue_System dialogue_System_Script;
    [SerializeField] private On_Interact on_Interact_Script;
    
    private bool triggered = false;

    
    
    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        dialogue_System_Script.Start_Dialogue();
        on_Interact_Script.Link_Puzzle_NPC();
    }

    

}