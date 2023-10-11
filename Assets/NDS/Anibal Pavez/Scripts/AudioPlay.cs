using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private InputsMap inputs;
    GunScript GunScript;
    private bool musicDeathOn, pauseMenu, timeAlmacenado, jumpset;
    private float timeMusic, volume;
    public static float pitch;
    CharacterController a;
    [SerializeField] public AudioClip[] SFX, ShootGunSFX, ReloadGun, Ambient, IntroMusic, MusicInGame, Steps, HeartLife, Jump, DamageLife;
    void Start()
    {
        //Ambiente inicial
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.4f);
        GunScript = GetComponent<GunScript>();
        a = GetComponent<CharacterController>();
        AudioManager.Instance.PlayMusic(MusicInGame[0]);
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void Update()
    {
        PausedGameMenuMusic();
        if (Movimiento.move.x < 0 || Movimiento.move.z < 0 || Movimiento.move.x > 0 || Movimiento.move.z > 0)
        {           
            //Velocidad de Pasos              
            if (Movimiento.isRunning)
            {
                volume = 1.4f;
                pitch = 1.5f;
                GunScript.AnimatorGun.SetFloat("VelocityGun", 2);
            }
            else { pitch = 1; GunScript.AnimatorGun.SetFloat("VelocityGun", 1); volume = 1; }
            if (Movimiento.isCrouching)
            {
                volume = 2.5f;
                pitch = 0.5f;
                GunScript.AnimatorGun.SetFloat("VelocityGun", 0.5f);
            }
            //Reproducir pasos
            if (a.isGrounded)
            {
                AudioManager.Instance.Step(Steps[Random.Range(0, Steps.Length)], volume, pitch);
                GunScript.AnimatorGun.SetBool("Walking", true);
                Debug.Log("Da un paso");
            }
        }
        else
        {
            GunScript.AnimatorGun.SetBool("Walking", false);
        }
        //Jump
        if (!pauseMenu)
        {
            if (Movimiento.isJump && !jumpset)
            {
                AudioManager.Instance.PlayGlobalSoundEffect(Jump[0], 2);
                jumpset = true;
            }
            if (a.isGrounded && Movimiento.isJump)
            {
                AudioManager.Instance.PlayGlobalSoundEffect(Jump[1]);
                Debug.Log("Salto"); Movimiento.isJump = false;
                jumpset = false;
            }
        }
       
        
        if (DeathScript.isDead)
        {
            if (!musicDeathOn)
            {
                //Musica de muerte
                AudioManager.Instance.PlayMusic(MusicInGame[4], 0.5f);
                //Campana de muerte
                AudioManager.Instance.PlayGlobalSoundEffect(SFX[1]);
            }

            musicDeathOn = true;
        }

    }
    private void PausedGameMenuMusic()
    {
        if (PauseScript.isPaused)
        {
            if (timeAlmacenado == false)
            {
                AudioManager.Instance.audioSourceAmbient.Pause();
                timeMusic = AudioManager.Instance.audioSourceMusic.time;
                timeAlmacenado = true;
            }
            if (!pauseMenu)
            {
                //Musica Pausa Inicia
                AudioManager.Instance.PlayMusic(MusicInGame[1], 1, 0, false);                
                Debug.Log("Reproduce Pausa");
                pauseMenu = true;
            }
            if (AudioManager.Instance.audioSourceMusic.isPlaying == false)
            {
                //Se reproduce Musica Pausa Completa con volumen ajustado
                AudioManager.Instance.PlayMusic(MusicInGame[2], 0.6f);
            }
        }
        if (PauseScript.isPaused == false && timeAlmacenado)
        {
            //Reanudamos musica MusicaInGame y m�sica ambiente continua sin cambios
            AudioManager.Instance.PlayMusic(MusicInGame[0], 1, timeMusic);
            AudioManager.Instance.audioSourceAmbient.UnPause();
            timeAlmacenado = false;
            pauseMenu = false;
            timeMusic = 0;
            Debug.Log("Reproduce INGAME");
        }
    }
    public void ShootingGun()
    {
        AudioManager.Instance.PlayGlobalSoundEffect(ShootGunSFX[Random.Range(0, ShootGunSFX.Length)]);
    }
    public void DamageLive()
    {
        AudioManager.Instance.PlayGlobalSoundEffect(DamageLife[Random.Range(0,DamageLife.Length)]);
    }
}
