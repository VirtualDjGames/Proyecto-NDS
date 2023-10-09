using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadArm : MonoBehaviour
{
    public AudioPlay AudioPlay;
    public void ReloadGunAudio()
    {
        AudioManager.Instance.PlayGlobalSoundEffect(AudioPlay.ReloadGun[0], 1, 1.5f);
    }
}
