using UnityEngine;

public class AidKit : MonoBehaviour
{
    //public AudioSource aidSound;

    void Update()
    {
        transform.Rotate(0, 0, 30 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            if(other.gameObject.GetComponent<Movimiento>().HP < 4)
            {
                Movimiento.isAttack = true;
                other.gameObject.GetComponent<Movimiento>().HP++;
                Destroy(gameObject);
            }
        }
    }
}
