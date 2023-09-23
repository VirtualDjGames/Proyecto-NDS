using System;
using UnityEngine;
using UnityEngine.UI;

public class DoorKey1 : MonoBehaviour
{
    public Transform targetPlayer;
    public AudioSource DoorSound;
    public Text aviso;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (((targetPlayer.position - transform.position).magnitude) <= 4)
        {
            if (Input.GetKey(KeyCode.E))
            { 
                if (Variables.Akeys >= 1)
                {
                    Variables.Akeys = -1;
                    transform.Rotate(0,75,0);
                    Destroy(aviso);
                    DoorSound.Play();
                }

            }
        }
    }
}
