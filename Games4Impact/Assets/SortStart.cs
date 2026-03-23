using UnityEngine;

public class SortStart : MonoBehaviour
{
    public GrabberArm grabberArm;
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        grabberArm.sortingManager.ShowNextShirt();
    }
}
