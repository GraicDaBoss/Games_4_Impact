using System.Collections;
using UnityEngine;

public class GrabberArm : MonoBehaviour
{
    [Header("References")]
    public Sorting_Manager sortingManager;
    public Transform arm;
    public Transform shirtAttachPoint;

    [Header("Positions")]
    public Transform centerPos;
    public Transform grabPos;
    public Transform recycleDropPos;
    public Transform reuseDropPos;

    [Header("Settings")]
    public float moveSpeed = 3f;

    private Transform currentShirt;
    private bool isAnimating = false;

    public void TriggerGrab()
    {
        if (!isAnimating)
            StartCoroutine(GrabAndCenter());
    }

    public void StartSequence(string zone)
    {
        if (!isAnimating)
            StartCoroutine(DropSequence(zone));
    }

    IEnumerator GrabAndCenter()
    {
        isAnimating = true;

        currentShirt = FindActiveShirt();
        if (currentShirt == null) { isAnimating = false; yield break; }

        // Move down to grab
        yield return StartCoroutine(MoveTo(grabPos.position));

        // Attach shirt
        currentShirt.SetParent(shirtAttachPoint);
        currentShirt.localPosition = Vector3.zero;

        // Move back to center
        yield return StartCoroutine(MoveTo(centerPos.position));

        isAnimating = false;
    }

    IEnumerator DropSequence(string zone)
    {
        isAnimating = true;

        bool isCorrect = currentShirt != null && (zone == currentShirt.tag);
        Transform dropPos = (zone == "Recycle") ? recycleDropPos : reuseDropPos;

        // Move to drop zone
        yield return StartCoroutine(MoveTo(dropPos.position));

        // Drop shirt
        if (currentShirt != null)
            currentShirt.SetParent(null);

        if (!isCorrect)
        {
            // Wrong — pick shirt back up and return to center
            yield return new WaitForSeconds(1f);
            if (currentShirt != null)
            {
                currentShirt.SetParent(shirtAttachPoint);
                currentShirt.localPosition = Vector3.zero;
            }
            yield return StartCoroutine(MoveTo(centerPos.position));
        }
        else
        {
            // Correct — return to center empty, then show and grab next shirt
            yield return StartCoroutine(MoveTo(centerPos.position));
            currentShirt = null;
            isAnimating = false;
            sortingManager.ShowNextShirt(); // this activates shirt AND calls TriggerGrab
            yield break;
        }

        isAnimating = false;
    }

    IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(arm.position, target) > 0.01f)
        {
            arm.position = Vector3.MoveTowards(arm.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        arm.position = target;
    }

    Transform FindActiveShirt()
    {
        foreach (var shirt in sortingManager.shirts)
            if (shirt.activeInHierarchy) return shirt.transform;
        return null;
    }
}