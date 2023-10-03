using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    [SerializeField] private AudioClip[] SFX, Ambient, IntroMusic, MusicInGame;
    void Start()
    {
        AudioManager.Instance.PlayAmbient(Ambient[0], 0.6f);
    }

}
