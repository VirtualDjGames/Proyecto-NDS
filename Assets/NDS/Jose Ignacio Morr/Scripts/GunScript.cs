using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    private InputsMap inputs;

    public int maxAmmo = 10; // Cantidad máxima de munición
    private int currentAmmo; // Munición actual
    public float reloadTime = 1.5f; // Tiempo de recarga en segundos
    private bool isReloading = false; // Está recargando

    // Start is called before the first frame update
    void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        currentAmmo = maxAmmo; // Inicializa la munición al máximo al inicio
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si está recargando
        if (isReloading)
        {
            return; // Si está recargando, no puede disparar
        }

        // Detecta el disparo
        if (inputs.Gameplay.Shoot.WasPressedThisFrame() && currentAmmo > 0)
        {
            Shoot(); // Llama a la función Shoot() si se presiona el botón de disparo y hay munición
        }

        // Detecta la recarga
        if (inputs.Gameplay.Reload.WasPressedThisFrame())
        {
            StartCoroutine(Reload()); // Inicia la rutina de recarga si se presiona el botón de recarga
        }
    }

    void Shoot()
    {
        // Realiza el disparo utilizando Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Verifica si el objeto golpeado tiene el tag "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                // Realiza aquí las acciones necesarias cuando se golpea a un enemigo
                // Por ejemplo, daño al enemigo

                Destroy(hit.collider.gameObject); //Prueba
            }
        }

        currentAmmo--; // Reduce la munición después de disparar
    }

    IEnumerator Reload()
    {
        // Comienza la recarga
        isReloading = true;

        // Espera durante el tiempo de recarga
        yield return new WaitForSeconds(reloadTime);

        // Llena la munición al máximo o al valor que desees
        currentAmmo = maxAmmo;

        // Finaliza la recarga
        isReloading = false;
    }
}

