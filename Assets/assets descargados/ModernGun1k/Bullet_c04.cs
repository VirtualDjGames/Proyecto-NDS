using UnityEditor;
using UnityEngine;

public class Bullet_c04 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform SpawnPoint;
    public Camera fpsCamera;
    public float currentCooldown = 0f;

    public AudioSource Shoot;
    public AudioSource Reloading;

    public GameObject recargaAdviser;

    //Potencia del Disparo (Alcance)
    public float Bullet_Forward_Force = 1000f;

    // Update is called once per frame
    void Update()
    {

        // Disparar
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= 0.2f)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) && Variables.Aammo > 0)
            {

                GameObject Bullet_Handler = Instantiate(bulletPrefab, SpawnPoint.position, Quaternion.identity) as GameObject;
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(SpawnPoint.forward * Bullet_Forward_Force);

                Variables.Aammo--;
                currentCooldown = 0;
                Shoot.Play();
            }


            // Recargar

                if (Variables.Rammo >= 8 && Variables.Aammo <= 0)
                {
                    recargaAdviser.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                    Variables.Aammo += 8;
                    Variables.Rammo -= 8;
                    recargaAdviser.SetActive(false);
                    Reloading.Play();
                    }
                }


        }


    }
}