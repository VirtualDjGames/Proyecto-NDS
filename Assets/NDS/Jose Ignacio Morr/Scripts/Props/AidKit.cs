using UnityEngine;

public class AidKit : MonoBehaviour
{
    //public AudioSource aidSound;

    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            //Añadir salud barra de salud depende de cuanto tenga
            Destroy(gameObject);
        }
    }
}
