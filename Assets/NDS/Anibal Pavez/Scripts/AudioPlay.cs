using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private InputsMap inputs;
    private bool musicDeathOn = true;
    CharacterController characterController;
    [SerializeField] private AudioClip[] SFX, Ambient, IntroMusic, MusicInGame, Steps, HeartLife;
    void Start()
    {
        //Ambiente inicial
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.4f);
        characterController = GetComponent<CharacterController>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void Update()
    {
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
}
