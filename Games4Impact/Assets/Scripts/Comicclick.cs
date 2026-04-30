using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ImagePanelController : MonoBehaviour
{
    [SerializeField] public Image targetPanel;
    [SerializeField] public Sprite[] imageArray;
    [SerializeField] public string sceneToLoad = "Scene1";

    private int currentImageIndex = 0;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        
        button.onClick.AddListener(OnButtonClick);

      
        targetPanel.sprite = imageArray[0];
    }

    private void OnButtonClick()
    {
        
        if (currentImageIndex >= imageArray.Length - 1)
        {
            
            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        
        currentImageIndex++;
        targetPanel.sprite = imageArray[currentImageIndex];
    }

    private void OnDestroy()
    {
        
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}