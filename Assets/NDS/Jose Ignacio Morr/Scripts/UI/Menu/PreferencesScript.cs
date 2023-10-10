using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesScript : MonoBehaviour
{
    public static float masterVolume; // Volumen maestro (0.0f a 1.0f)
    public static float musicVolume;  // Volumen de la música (0.0f a 1.0f)
    public static float effectsVolume; // Volumen de efectos (0.0f a 1.0f)

    [Header("Audio")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    void Awake()
    {
        // Cargar ajustes de sonido desde PlayerPrefs
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", masterVolume);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", effectsVolume);
    }

    void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }

    public void SetMasterVolume(float volume)
    {
        float newVolume = masterSlider.value; // Obtén el valor actual del slider
        masterVolume = newVolume; // Actualiza la variable de volumen maestro
        // Aplica el nuevo volumen a tu sistema de audio o controladores de sonido aquí
        // Ejemplo: AudioListener.volume = masterVolume;

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        float newVolume = musicSlider.value; // Obtén el valor actual del slider
        musicVolume = newVolume; // Actualiza la variable de volumen maestro
        // Aplica el nuevo volumen a tu sistema de audio o controladores de sonido aquí
        // Ejemplo: AudioListener.volume = masterVolume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetEffectsVolume(float volume)
    {
        float newVolume = effectsSlider.value; // Obtén el valor actual del slider
        effectsVolume = newVolume; // Actualiza la variable de volumen maestro
        // Aplica el nuevo volumen a tu sistema de audio o controladores de sonido aquí
        // Ejemplo: AudioListener.volume = masterVolume;

        PlayerPrefs.SetFloat("EffectsVolume", volume);
        PlayerPrefs.Save();
    }
}
