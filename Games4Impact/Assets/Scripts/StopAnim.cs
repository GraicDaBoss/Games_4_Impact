using UnityEngine;
using UnityEngine.Animations;

public class StopAnim : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.StopPlayback();
        }
    }
}
