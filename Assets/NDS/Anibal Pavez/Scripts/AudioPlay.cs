using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField] private AudioClip[] clip, piano;
    [SerializeField] private AudioClip[] music, ambient;
    float tiempoInicial, tiempoEsperado = 2f;
    int cambioMusica;

    void Start()
    {
        AudioManager.Instance.PlayMusic(music[0]);
    }
    private void Update()
    {
        if (AudioManager.Instance.audioSourceMusic.isPlaying == false)
        {
            if (cambioMusica == 1)
            {
                cambioMusica = -1;
            }

            AudioManager.Instance.PlayMusic(music[++cambioMusica]);
            Debug.Log("Cambio de Música");
           
        }

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Puerta1"))
        {
            AudioManager.Instance.PlayGlobalSoundEffect(clip[0]);
        }
        if (collider.CompareTag("Fuera"))
        {
            AudioManager.Instance.PlayAmbient(ambient[0]);
        }
        if (collider.CompareTag("salondor"))
        {
            AudioManager.Instance.PlayGlobalSoundEffect(clip[1]);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Casa"))
        {
            AudioManager.Instance.VolumeAmbient(0.2f);
            Debug.Log("ESTOY BAJO");
        }
        tiempoInicial += Time.deltaTime;
        if (other.CompareTag("Piano") && tiempoInicial >= tiempoEsperado)
        {
            int Nota = Random.Range(0, 9);
            AudioManager.Instance.PlayGlobalSoundEffect(piano[Nota]);
            tiempoInicial = 0;
            tiempoEsperado = Random.Range(0.2f, 1f);
        }

    }
    private void OnTriggerExit(Collider collider1)
    {
        if (collider1.CompareTag("Casa"))
        {
            AudioManager.Instance.VolumeAmbient(1f);
        }
        if (collider1.CompareTag("Fuera"))
        {
            AudioManager.Instance.PlayAmbient(ambient[1]);
        }
    }
}
