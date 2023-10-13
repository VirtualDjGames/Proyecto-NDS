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
    public AudioSource audioSourceAmbientShot; //AmbienteShot
    public AudioSource audioSteps; //Gestion de Pasos
    public AudioSource audioHeart; //Gestion de Corazon
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
	public void PlayGlobalSoundEffect(AudioClip clip, float volume = 1f, float pitch = 1)
    {
		audioSourceSFX.pitch = pitch;
		audioSourceSFX.PlayOneShot(clip, volume);
		
    }
    public void PlayMusic(AudioClip clip, float volume = 1, float time = 0, bool loop = true)
    {

        audioSourceMusic.clip = clip;
        audioSourceMusic.volume = volume;
		audioSourceMusic.time = time;
		audioSourceMusic.loop = loop;
        audioSourceMusic.Play();
    }

    public void PlayAmbient(AudioClip clip, float volume = 1, bool loop = true)
    {
        audioSourceAmbient.loop = loop;
        audioSourceAmbient.clip = clip;
        audioSourceAmbient.volume = volume;
        audioSourceAmbient.Play();
    }
    public void PlayAmbientShot(AudioClip clip, float volume = 1, bool loop = true)
    {      
        audioSourceAmbientShot.PlayOneShot(clip, volume);
    }
    public void Step(AudioClip clip, float volume = 1, float pitch = 1)
    {
        if (!audioSteps.isPlaying)
        {
            audioSteps.clip = clip;
            audioSteps.volume = volume;
            audioSteps.pitch = pitch;
            audioSteps.Play();
        }
    }
    public void Heart(AudioClip clip, float volume = 1, float pitch = 1)
    {
        audioHeart.clip = clip;
        audioHeart.volume = volume;
        audioHeart.pitch = pitch;
        audioHeart.Play();
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
	public void SetMusicAmbientVolume(float volume)
    {
        master.SetFloat("volMusic", Mathf.Log10(volume) * 20);
		master.SetFloat("volAmbient", Mathf.Log10(volume) * 20);
	}  
	public void SetMasterVolume(float volume)
    {
		master.SetFloat("MasterAudio", Mathf.Log10(volume) * 20);
    }
}

