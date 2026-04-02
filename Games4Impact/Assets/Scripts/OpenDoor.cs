using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    public int sceneIndex = 1;
    public GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetObject)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}