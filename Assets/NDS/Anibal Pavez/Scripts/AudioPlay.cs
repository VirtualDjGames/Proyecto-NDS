using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private InputsMap inputs;
    private bool musicDeathOn, pauseMenu, timeAlmacenado;
    private float timeMusic;
    CharacterController characterController;
    [SerializeField] private AudioClip[] SFX, Ambient, IntroMusic, MusicInGame, Steps, HeartLife;
    void Start()
    {
        //Ambiente inicial
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.4f);
        AudioManager.Instance.PlayMusic(MusicInGame[0]);
        characterController = GetComponent<CharacterController>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void Update()
    {
        PausedGameMenuMusic();
        if (Movimiento.move.x < 0 || Movimiento.move.z < 0 || Movimiento.move.x > 0 || Movimiento.move.z > 0)
        {
            //Pasos  
            AudioManager.Instance.Step(Steps[Random.Range(0, Steps.Length)]);
            Debug.Log("Da un paso");
        }
        if (DeathScript.isDead)
        {
            if (musicDeathOn)
            {
                //Musica de muerte
                AudioManager.Instance.PlayMusic(MusicInGame[4], 0.5f);
                //Campana de muerte
                AudioManager.Instance.PlayGlobalSoundEffect(SFX[1]);
            }

            musicDeathOn = false;
        }

    }
    private void PausedGameMenuMusic()
    {
        if (PauseScript.isPaused)
        {
            if (timeAlmacenado == false)
            {
                timeMusic = AudioManager.Instance.audioSourceMusic.time;
                timeAlmacenado = true;
            }
            if (!pauseMenu)
            {
                AudioManager.Instance.PlayMusic(MusicInGame[1], 1, 0, false);
                Debug.Log("Reproduce Pausa");
                pauseMenu = true;
            }
            if (AudioManager.Instance.audioSourceMusic.isPlaying == false)
            {
                AudioManager.Instance.PlayMusic(MusicInGame[2]);
            }
        }
        if (PauseScript.isPaused == false && timeAlmacenado)
        {
            AudioManager.Instance.PlayMusic(MusicInGame[0], 1, timeMusic);
            timeAlmacenado = false;
            pauseMenu = false;
            timeMusic = 0;
            Debug.Log("Reproduce INGAME");
        }
    }
}
