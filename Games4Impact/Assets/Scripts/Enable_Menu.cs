using UnityEngine;

public class Enable_Menu : MonoBehaviour
{
    [SerializeField] private GameObject to_Disable;
    [SerializeField] private GameObject to_Enable;


    public void Button_Pressed()
    {
        to_Enable.SetActive(true);
        to_Disable.SetActive(false);
    }
}
