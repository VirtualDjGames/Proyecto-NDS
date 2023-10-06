using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public AudioSource audioSourceSFX; //Efectos de sonido
	public AudioSource audioSourceMusic; //Musica
	public AudioSource audioSourceAmbient; //Ambiente
										   //private AudioSource audioSourceAmbient; //Sonido ambiente EJ:LLuvia
	public AudioMixer master;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			//audioSourceAmbient = gameObject.AddComponent<AudioSource>();
		}
		else
		{
			Destroy(Instance);
			Debug.LogError("Mas de una instancia");
			Debug.LogError("Revisa tu escena >:(");
		}

	}
	public void PlayGlobalSoundEffect(AudioClip clip, float volume = 1f)
	{
		audioSourceSFX.PlayOneShot(clip, volume);
	}
	public void PlayMusic(AudioClip clip)
	{

		audioSourceMusic.clip = clip;
		audioSourceMusic.Play();
    }

    public void PlayAmbient(AudioClip clip, float volume = 1, bool loop = true)
    {
        audioSourceAmbient.loop = loop;
        audioSourceAmbient.clip = clip;
        audioSourceAmbient.volume = volume;
        audioSourceAmbient.Play();
    }
	public void Step(AudioClip clip, float volume = 1, float time = 1 )
    {
        if (audioSourceSFX.isPlaying == false)
        {
			audioSourceSFX.clip = clip;
			audioSourceSFX.volume = volume;
			audioSourceSFX.pitch = time;
			audioSourceSFX.Play();
			
		}
        
	}
    public void VolumeAmbient(float volume)
    {
        audioSourceAmbient.volume = volume;
    }
	//Manejo de Volumen por UI, haciendo uso de AudioMixer
    public void SetSoundEffectsVolume(float volume)
	{
		master.SetFloat("volSFX", Mathf.Log10(volume) * 20);
	}
	public void SetMusicVolume(float volume)
	{
		master.SetFloat("volMusic", Mathf.Log10(volume) * 20);
	}
	public void SetAmbientVolume(float volume)
	{
		master.SetFloat("volAmbient", Mathf.Log10(volume) * 20);
	}
	public void SetMasterVolume(float volume)
    {
		master.SetFloat("MasterAudio", Mathf.Log10(volume) * 20);
    }
}

