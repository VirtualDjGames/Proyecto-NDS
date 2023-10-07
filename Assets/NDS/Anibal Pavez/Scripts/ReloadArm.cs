using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArm : MonoBehaviour
{
    public AudioPlay AudioPlay;
    public void ReloadGunAudio()
    {
        AudioManager.Instance.PlayGlobalSoundEffect(AudioPlay.ReloadGun[Random.Range(0, AudioPlay.ReloadGun.Length)]);
    }
}
