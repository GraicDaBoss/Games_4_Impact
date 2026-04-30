using UnityEngine;

public class Play_Audio : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audio_Source;
    
    [Header("Walk Steps")] 
    [SerializeField] private AudioClip[] step_Sounds;
    
    [Header("Jump")]
    [SerializeField] private AudioClip jump_Sound;
    
    [Header("Landing")]
    [SerializeField] private AudioClip land_Sound;
    
    public void Play_Step_Sound()
    {
        int clip = Random.Range(0, step_Sounds.Length - 1);
        audio_Source.PlayOneShot(step_Sounds[clip]);
    }

    public void Play_Jump_Sound()
    {
        audio_Source.PlayOneShot(jump_Sound);
    }

    public void Play_Land_Sound()
    {
        audio_Source.PlayOneShot(land_Sound);
    }
    
}
