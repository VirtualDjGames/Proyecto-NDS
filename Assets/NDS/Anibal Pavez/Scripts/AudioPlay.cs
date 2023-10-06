using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private InputsMap inputs;
    CharacterController characterController;
    [SerializeField] private AudioClip[] SFX, Ambient, IntroMusic, MusicInGame, Steps;
    void Start()
    {
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.6f);
        characterController = GetComponent<CharacterController>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void Update()
    {
        if (Movimiento.move.x < 0 || Movimiento.move.z < 0 || Movimiento.move.x > 0 || Movimiento.move.z > 0)
        {
            
                AudioManager.Instance.Step(Steps[Random.Range(0, Steps.Length)]);
                Debug.Log("Da un paso");
            
            
        }
    }
}
