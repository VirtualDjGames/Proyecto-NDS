using UnityEngine;

public class BoxAmmo : MonoBehaviour
{
    //public AudioSource ammoSound;

    void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            //Añadir munición
            Destroy(gameObject);
        }
    }
}

