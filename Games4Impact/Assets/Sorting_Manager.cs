using System.Collections;
using UnityEngine;
using TMPro;

public class Sorting_Manager : MonoBehaviour
{
    
    public GameObject[] shirts;

    
    public TextMeshProUGUI feedbackText;

    private int currentIndex = 0;
    private GameObject currentShirt;

    void Start()
    {
        foreach (var shirt in shirts)
            shirt.SetActive(false);

        ShowNextShirt();
    }

    void ShowNextShirt()
    {
        if (currentIndex >= shirts.Length)
        {
            feedbackText.text = "All done!";
            feedbackText.color = Color.white;
            return;
        }

        currentShirt = shirts[currentIndex];
        currentShirt.SetActive(true);
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
            currentIndex++;
            StartCoroutine(NextShirtDelay());
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

    IEnumerator NextShirtDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ShowNextShirt();
    }
}