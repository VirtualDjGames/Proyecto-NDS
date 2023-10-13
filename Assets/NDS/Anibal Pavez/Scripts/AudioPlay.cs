using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private InputsMap inputs;
    GunScript GunScript;
    Movimiento MovimientoScript;
    private bool musicDeathOn, pauseMenu, timeAlmacenado, jumpset, startHeart;
    private float timeMusic, volume, timeToGame, pitchHeart;
    public static float pitch;
    CharacterController CharacterController;
    [SerializeField] public AudioClip[] SFX, ShootGunSFX, ReloadGun, Ambient, IntroMusic, MusicInGame, Steps, HeartLife, Jump, DamageLife;
    void Start()
    {
        //Ambiente inicial
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.4f);
        GunScript = GetComponent<GunScript>();
        CharacterController = GetComponent<CharacterController>();
        MovimientoScript = GetComponent<Movimiento>();
        AudioManager.Instance.PlayMusic(MusicInGame[0]);
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void Update()
    {
        PausedGameMenuMusic();
        HeartSound();
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
            if (CharacterController.isGrounded)
            {
                AudioManager.Instance.Step(Steps[Random.Range(0, Steps.Length)], volume, pitch);
                GunScript.AnimatorGun.SetBool("Walking", true);
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
            if (CharacterController.isGrounded && Movimiento.isJump)
            {
                AudioManager.Instance.PlayGlobalSoundEffect(Jump[1]);
                Movimiento.isJump = false;
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
                //Grito de Muerte
                AudioManager.Instance.PlayGlobalSoundEffect(HeartLife[1]);
                //Detemos el corazon
                AudioManager.Instance.audioHeart.Stop();
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
                AudioManager.Instance.audioSourceSFX.Pause();
                AudioManager.Instance.audioHeart.Pause();
                AudioManager.Instance.audioSourceAmbientShot.Pause();
                AudioManager.Instance.audioSteps.Pause();
                timeMusic = AudioManager.Instance.audioSourceMusic.time;
                Debug.Log(timeMusic);
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
        else if (timeAlmacenado)
        {
            //Reanudamos musica MusicaInGame y música ambiente continua sin cambios
            AudioManager.Instance.PlayMusic(MusicInGame[0], 1, timeMusic);
            AudioManager.Instance.audioSourceAmbient.UnPause();
            AudioManager.Instance.audioSourceSFX.UnPause();
            AudioManager.Instance.audioHeart.UnPause();
            AudioManager.Instance.audioSourceAmbientShot.UnPause();
            AudioManager.Instance.audioSteps.UnPause();
            timeAlmacenado = false;
            pauseMenu = false;
            timeMusic = 0;            
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
    public void HeartSound()
    {
        if (MovimientoScript.HP == 4 && AudioManager.Instance.audioHeart.isPlaying)
        {
            //Calmar heart
            timeToGame += Time.deltaTime;
            float newPitch = Mathf.Lerp(pitchHeart, 0.7f, timeToGame / 3);
            AudioManager.Instance.audioHeart.pitch = newPitch;
            //Atenuacion de Heart
            if (AudioManager.Instance.audioHeart.pitch == 0.7f)
            {
                float newVolume = Mathf.Lerp(pitchHeart, 0.0001f, timeToGame / 3);
                AudioManager.Instance.audioHeart.volume = newVolume;
            }
        }
        //Detener Sound
        if (AudioManager.Instance.audioHeart.volume <= 0.0001f && startHeart)
        {
            AudioManager.Instance.audioHeart.Stop();
            timeToGame = 0;
            startHeart = false;
        }
        if (MovimientoScript.HP <= 3)
        {
            if (!startHeart && MovimientoScript.HP != 0)
            {
                pitchHeart = 1;
                AudioManager.Instance.Heart(HeartLife[0], 1, pitchHeart);
                startHeart = true;
            }
            if (MovimientoScript.HP == 2)
            {
                pitchHeart = 1.6f;
                AudioManager.Instance.audioHeart.pitch = pitchHeart;
                AudioManager.Instance.audioHeart.volume = 1.5f;
            }
            if (MovimientoScript.HP == 1)
            {
                pitchHeart = 2f;
                AudioManager.Instance.audioHeart.pitch = pitchHeart;
                AudioManager.Instance.audioHeart.volume = 2f;
            }
        }

    }
    public void Keys()
    {
        AudioManager.Instance.PlayGlobalSoundEffect(SFX[2]);
    }
    public void AmbientSound(int Sound, float volume)
    {
        AudioManager.Instance.PlayAmbientShot(Ambient[Sound],volume);
    }
    public void AmbientSoundSuspenso(int Sound, float volume)
    {
        AudioManager.Instance.PlayAmbient(Ambient[Sound], volume);
    }
}
