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
    public int current_Dialogue_I = 0;
    private int current_Line_I = 0;
    private bool currently_Typing = false;

    [Header("UI Functionality")]
    [SerializeField] private GameObject dialogue_Box;
    [SerializeField] private TextMeshProUGUI text_Component;
    [SerializeField] private float text_Speed;

    [Header("Other UI To Disable")]
    [SerializeField] private GameObject[] otherUI; // assign in Inspector

    [Header("Restrict Player Controls")]
    [SerializeField] private PlayerWalk player_Controls;
    [SerializeField] private On_Interact interact_Script;
    public bool in_Dialogue = false;

    [Header("Restrict Camera Controls")]
    public GameObject dialogue_Camera_Position;
    private camerafollow camera_Script;

    [Header("Objective Camera Controls")]
    [SerializeField] private GameObject objective_camera_Position;
    [SerializeField] private GameObject objective_Object;
    public bool pan_To_Objective = false;
    public bool pan_On_Next_Line = false;

    private void Start()
    {
        camera_Script = Camera.main.GetComponent<camerafollow>();
    }

    // 🔹 Enable/Disable all other UI
    private void SetOtherUI(bool state)
    {
        foreach (GameObject ui in otherUI)
        {
            if (ui != null)
            {
                ui.SetActive(state);
                Debug.Log($"UI {ui.name} set to {state}");
            }
            else
            {
                Debug.LogWarning("Null UI reference in otherUI array");
            }
        }
    }

    public void Start_Dialogue()
    {
        (current_Dialogue, pan_On_Next_Line) = character_Script.Get_Dialogue(current_Dialogue_I);

        if (current_Dialogue == null)
        {
            Debug.LogError("ERROR: Dialogue not found");
            return;
        }

        interact_Script.npc_Can_Interact = true;

        // Camera setup
        Vector3 camera_Target_Position = dialogue_Camera_Position.transform.position;
        camera_Script.Pan_To_Dialogue(camera_Target_Position, this.transform.position);

        // Start dialogue
        Toggle_Dialogue();
        StartCoroutine(Type_Line());
    }

    IEnumerator Type_Line()
    {
        text_Component.text = "";
        currently_Typing = true;

        foreach (char c in current_Dialogue[current_Line_I].ToCharArray())
        {
            text_Component.text += c;
            yield return new WaitForSeconds(text_Speed);
        }

        currently_Typing = false;
        current_Line_I++;
    }

    public void Dialogue_Interacted()
    {
        if (currently_Typing)
        {
            StopAllCoroutines();
            text_Component.text = current_Dialogue[current_Line_I];
            currently_Typing = false;
            current_Line_I++;
        }
        else if (current_Line_I >= current_Dialogue.Length)
        {
            if (current_Dialogue_I != 1)
                current_Dialogue_I = 1;

            camera_Script.Pan_From_Dialogue();

            // 🔥 Ends dialogue (this also re-enables UI)
            Toggle_Dialogue();

            interact_Script.npc_Can_Interact = false;
        }
        else
        {
            if (pan_To_Objective)
            {
                camera_Script.mid_Pan = true;

                Vector3 camera_Target_Position = objective_camera_Position.transform.position;
                camera_Script.Pan_To_Dialogue(camera_Target_Position, objective_Object.transform.position);
            }

            StartCoroutine(Type_Line());

            if (pan_On_Next_Line)
            {
                pan_On_Next_Line = false;
                pan_To_Objective = true;
            }
        }
    }

    private void Toggle_Dialogue()
    {
        in_Dialogue = !in_Dialogue;

        // Show/hide dialogue UI
        dialogue_Box.SetActive(in_Dialogue);

        // 🔥 THIS controls all other UI
        SetOtherUI(!in_Dialogue);

        text_Component.text = "";
        player_Controls.in_Dialogue = in_Dialogue;

        if (in_Dialogue)
            current_Line_I = 0;
    }
}