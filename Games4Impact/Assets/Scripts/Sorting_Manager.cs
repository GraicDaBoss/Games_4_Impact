using System.Collections;
using UnityEngine;
using TMPro;

public class Sorting_Manager : MonoBehaviour
{
    public GameObject[] shirts;
    public TextMeshProUGUI feedbackText;
    public GrabberArm grabberArm;
    public GameObject endObject; 

    private int currentIndex = 0;
    public GameObject currentShirt { get; private set; }

    void Start()
    {
        foreach (var shirt in shirts)
            shirt.SetActive(false);
    }
    public void ShowNextShirt()
    {
        if (currentIndex >= shirts.Length)
        {
            feedbackText.text = "All done!";
            feedbackText.color = Color.white;

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

        string correctAnswer = currentShirt.tag;
        bool isCorrect = (zoneTag == correctAnswer);
        Debug.Log($"Zone Tag: {zoneTag}, Shirt Tag: {correctAnswer}");
        if (isCorrect)
        {
            StartCoroutine(ShowFeedback(true));
            currentIndex++;
            // DON'T deactivate here — arm will handle it
        }
        else
        {
            StartCoroutine(ShowFeedback(false));
        }
    }

    IEnumerator ShowFeedback(bool correct)
    {
        feedbackText.text = correct ? "Correct!" : "Wrong, try again!";
        feedbackText.color = correct ? Color.green : Color.red;
        yield return new WaitForSeconds(1.5f);
        feedbackText.text = "";
    }
}