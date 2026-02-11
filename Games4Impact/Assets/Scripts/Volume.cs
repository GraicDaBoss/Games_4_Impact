using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    private const string VOLUME_PARAM = "MasterVolume";

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        slider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;

        mixer.SetFloat(VOLUME_PARAM, volume);
        PlayerPrefs.SetFloat("volume", value);
    }
}

