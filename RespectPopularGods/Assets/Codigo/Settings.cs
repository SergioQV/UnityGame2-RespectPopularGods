using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Tooltip("AudioSource del Juego")]
    public AudioSource audioSource;
    [Tooltip("Botones")]
    public GameObject MuteOn, MuteOff;

    public Slider SliderVolumen;

    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        SliderVolumen.value = PlayerPrefs.GetFloat("volume");

        // Cargar Toggler del Mute
        if (PlayerPrefs.GetString("mute").Equals("on"))
        {
            MuteOff.SetActive(true);
            MuteOn.SetActive(false);
            SliderVolumen.value = 0;
        }
        else
        {
            MuteOff.SetActive(false);
            MuteOn.SetActive(true);
            SliderVolumen.value = PlayerPrefs.GetFloat("volume");
        }

    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetMuteOn()
    {
        PlayerPrefs.SetFloat("volumeAux", PlayerPrefs.GetFloat("volume"));
        audioSource.volume = 0;
        PlayerPrefs.SetString("mute", "on");
        SliderVolumen.value = 0;
        PlayerPrefs.Save();
    }

    public void SetMuteOff()
    {
        audioSource.volume = PlayerPrefs.GetFloat("volumeAux");
        PlayerPrefs.SetString("mute", "off");
        SliderVolumen.value = PlayerPrefs.GetFloat("volumeAux");
        PlayerPrefs.Save();
    }
    
    void Update()
    {
        // Actualizar Toggler del Volumen cuando este se pone a "0"
        if (PlayerPrefs.GetFloat("volume") == 0)
        {
            MuteOff.SetActive(true);
            MuteOn.SetActive(false);
        }
        else
        {
            MuteOff.SetActive(false);
            MuteOn.SetActive(true);
        }
    }

}