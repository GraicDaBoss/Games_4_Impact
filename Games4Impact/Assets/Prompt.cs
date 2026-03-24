using UnityEngine;

public class Prompt : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
            target.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (target != null)
            target.SetActive(false);
    }
}