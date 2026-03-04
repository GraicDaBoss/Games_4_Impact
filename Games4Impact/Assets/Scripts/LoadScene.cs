using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] public string sceneName;

    public void Load()
    {
       
        SceneManager.LoadScene(sceneName);
        
    }
}

