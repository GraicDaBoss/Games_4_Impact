using System.Collections;
using UnityEngine;
using TMPro;

public class Sorting_Manager : MonoBehaviour
{
    public GameObject[] shirts;
    public TextMeshProUGUI feedbackText;
    public GrabberArm grabberArm;

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
            return;
        }

        currentShirt = shirts[currentIndex];
        currentShirt.SetActive(true);

        // Trigger arm to grab now that shirt is active
        grabberArm.TriggerGrab();
    }

    public void PlayerChose(string zoneTag)
    {
        if (currentShirt == null) return;
        string correctAnswer = currentShirt.tag;
        bool isCorrect = (zoneTag == correctAnswer);

        if (isCorrect)
        {
            StartCoroutine(ShowFeedback(true));
            currentShirt.SetActive(false);
            currentShirt = null;
            currentIndex++;
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