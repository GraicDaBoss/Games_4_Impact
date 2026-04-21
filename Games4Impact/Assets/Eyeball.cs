using UnityEngine;

public class Eyeball : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}
