using UnityEngine;

public class Doorkey2 : MonoBehaviour
{
    public Transform targetPlayer;
    public AudioSource DoorSound;
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
                if (Variables.Bkeys >= 1)
                {
                    Variables.Bkeys = -1;
                    transform.Rotate(0, 75, 0);
                    DoorSound.Play();
                }




            }
        }
    }
}
