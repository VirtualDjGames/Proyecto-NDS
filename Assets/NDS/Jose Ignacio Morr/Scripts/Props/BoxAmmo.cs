using UnityEngine;

public class BoxAmmo : MonoBehaviour
{
    //public AudioSource ammoSound;

    void Update()
    {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            Movimiento.isAttack = true;
            other.gameObject.GetComponent<GunScript>().currentReserveAmmo += 10;
            Destroy(gameObject);
        }
    }
}

