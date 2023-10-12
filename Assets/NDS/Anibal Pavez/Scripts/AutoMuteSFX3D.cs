using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMuteSFX3D : MonoBehaviour
{
    AudioSource AudioSource;
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
}
