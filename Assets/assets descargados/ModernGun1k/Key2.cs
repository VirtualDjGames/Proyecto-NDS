using UnityEngine;

public class Key2 : MonoBehaviour
{
    public Transform targetPlayer;
    public AudioSource KeySound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (((targetPlayer.position - transform.position).magnitude) <= 2)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Variables.Bkeys = +1;
                Destroy(gameObject);
                KeySound.Play();
            }
        }
    }
}
