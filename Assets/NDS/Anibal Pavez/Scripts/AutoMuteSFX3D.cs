using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMuteSFX3D : MonoBehaviour
{
    AudioSource AudioSource;
    public AudioClip[] Door;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.time = Random.Range(0, 40);
    }
    private void Update()
    {
        if (PauseScript.isPaused || DeathScript.isDead)
        {
            AudioSource.Pause();
        }
        else
        {
            AudioSource.UnPause();
        }
    }
    public void OpenDoor()
    {
        AudioSource.time = 0;
        AudioSource.PlayOneShot(Door[0]);
        AudioSource.PlayOneShot(Door[1]);
    }
    public void CloseDoorSound()
    {
        AudioSource.time = 0;
        AudioSource.PlayOneShot(Door[2]);
    }
}
