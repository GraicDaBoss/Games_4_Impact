using System.Collections;
using UnityEngine;

public class GrabberArm : MonoBehaviour
{
    
    public Sorting_Manager sortingManager;
    public Transform arm;
    public Transform shirtAttachPoint;

    
    public Transform centerPos;
    public Transform grabPos;
    public Transform recycleDropPos;
    public Transform reuseDropPos;

    
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

        
        yield return StartCoroutine(MoveTo(grabPos.position));

        
        currentShirt.SetParent(shirtAttachPoint);
        currentShirt.localPosition = Vector3.zero;

        yield return StartCoroutine(MoveTo(centerPos.position));

        isAnimating = false;
    }

    IEnumerator DropSequence(string zone)
    {
        isAnimating = true;
        bool isCorrect = currentShirt != null && (zone == currentShirt.tag);
        Transform dropPos = (zone == "Recycle") ? recycleDropPos : reuseDropPos;

        yield return StartCoroutine(MoveTo(dropPos.position));

        if (currentShirt != null)
            currentShirt.SetParent(null);
        if (!isCorrect)
        {
            yield return new WaitForSeconds(1f);
            if (currentShirt != null)
            {
                currentShirt.SetParent(shirtAttachPoint);
                currentShirt.localPosition = Vector3.zero;
            }
            yield return StartCoroutine(MoveTo(centerPos.position));
            isAnimating = false;  
        }
        else
        {
            
            if (currentShirt != null)
            {
                currentShirt.SetParent(null);
                Rigidbody rb = currentShirt.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.isKinematic = false;
            }
          
            yield return StartCoroutine(MoveTo(centerPos.position));
            if (currentShirt != null)
                currentShirt.gameObject.SetActive(false);
            currentShirt = null;
            isAnimating = false;
            sortingManager.ShowNextShirt();
        }
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