using UnityEngine;

public class BoxAmmo : MonoBehaviour
{ public Transform targetPlayer;
    public AudioSource ammoSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);

        if (((targetPlayer.position - transform.position).magnitude) <= 2)
        {
            {
                Destroy(gameObject);
                Variables.Rammo += 8;
                ammoSound.Play();
            }
        }
    }
}

