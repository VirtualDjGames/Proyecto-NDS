using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public Transform targetPlayer;
    public AudioSource aidSound;
    void Start()
    {

    }


    void Update()
    {
        transform.Rotate(0, 0, 1);

        if (((targetPlayer.position - transform.position).magnitude) <= 1.5)
        {
            if (Variables.hp < 100)
            {
                Destroy(gameObject);
                Variables.hp += 25;
                aidSound.Play();
            }
        }
    }
}
