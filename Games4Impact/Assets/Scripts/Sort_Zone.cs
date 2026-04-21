using UnityEngine;

public class Sort_Zone : MonoBehaviour
{
    public string zoneTag;
    public Sorting_Manager manager;
    public GrabberArm grabberArm;
    public GameObject interactButton;

    private bool playerInside = false;

    void Start()
    {
        if (interactButton != null)
            interactButton.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
        if (interactButton != null)
            interactButton.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
        if (interactButton != null)
            interactButton.SetActive(false);
    }

    public void OnInteractPressed()
    {
        print("interact");
        if (!playerInside) return;
        print("player in zone");
        manager.PlayerChose(zoneTag);
        grabberArm.StartSequence(zoneTag);
    }
}