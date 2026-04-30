using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class Sorting_Manager : MonoBehaviour
{
    public GameObject[] shirts;
    public GrabberArm grabberArm;
    public GameObject endObject; 

    private int currentIndex = 0;
    public GameObject currentShirt { get; private set; }

    
    [Header("Dialogue")]
    [SerializeField] private Dialogue_System dialogue_System;
    [SerializeField] private GameObject[] dialogue_Camera_Positions;
    
    
    void Start()
    {
        foreach (var shirt in shirts)
            shirt.SetActive(false);
    }
    public void ShowNextShirt()
    {
        if (currentIndex >= shirts.Length)
        {
            dialogue_System.dialogue_Camera_Position = dialogue_Camera_Positions[0];
            dialogue_System.current_Dialogue_I = 4;
            dialogue_System.Start_Dialogue();

            if (endObject != null)
                endObject.SetActive(false);

            return;
        }

        currentShirt = shirts[currentIndex];
        currentShirt.SetActive(true);
        grabberArm.TriggerGrab();
    }

    public void PlayerChose(string zoneTag)
    {
        if (currentShirt == null) return;
        dialogue_System.dialogue_Camera_Position = dialogue_Camera_Positions[1];
        string correctAnswer = currentShirt.tag;
        bool isCorrect = (zoneTag == correctAnswer);
        Debug.Log($"Zone Tag: {zoneTag}, Shirt Tag: {correctAnswer}");
        if (isCorrect)
        {
            
            dialogue_System.current_Dialogue_I = 2;
            dialogue_System.Start_Dialogue();
            currentIndex++;
            // DON'T deactivate here — arm will handle it
        }
        else
        {
            dialogue_System.current_Dialogue_I = 3;
            dialogue_System.Start_Dialogue();
        }
    }


}