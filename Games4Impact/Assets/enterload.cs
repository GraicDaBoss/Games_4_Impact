using UnityEngine;
using UnityEngine.SceneManagement;

public class enterload : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public string sceneName;

    private void OnTriggerEnter(Collider other)

    {
        SceneManager.LoadScene(sceneName);
    }

}
