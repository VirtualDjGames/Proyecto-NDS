using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEE : MonoBehaviour
{
    public Transform targetPlayer;
    public AudioSource easterSong;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (((targetPlayer.position - transform.position).magnitude) <= 3.5)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Destroy(gameObject);
                easterSong.Play();
            }
        }
    }
}
