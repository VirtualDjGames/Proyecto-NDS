using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator Animator;
    public AutoMuteSFX3D AutoMuteSFX3D;
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && Animator.GetBool("isOpen") == true)
        {
            Animator.SetBool("isOpen", false);
            AutoMuteSFX3D.CloseDoorSound();
        }
    }
}
