using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    private InputsMap inputs;

    public int maxAmmo = 10; // Cantidad m�xima de munici�n activa
    private int currentAmmo; // Munici�n activa actual

    public int currentReserveAmmo; // Munici�n de reserva actual

    private float reloadTime; // Tiempo de recarga en segundos
    private bool isReloading = false; // Est� recargando

    private float shootTime; // Tiempo de recarga en segundos
    private bool isShooting = false;

    public TextMeshProUGUI ammoUI;
    public Image reloadingImage;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        reloadingImage.fillAmount = 0.0f;
        reloadingImage.gameObject.SetActive(false);
        currentAmmo = maxAmmo;

        shootTime = Mathf.Clamp(shootTime, 0, 5.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1) // Si el juego no est� pausado
        {
            shootTime += Time.deltaTime;

            // Detecta el disparo
            if (inputs.Gameplay.Shoot.WasPressedThisFrame() && currentAmmo > 0 && !isReloading && shootTime > 0.5f)
            {
                Shoot();
                isShooting = true;
                shootTime = 0;
            }

            // Detecta la recarga
            if (inputs.Gameplay.Reload.WasPressedThisFrame())
            {
                Reload();
            }

            if(isShooting)
            {
                HUDScript.Instance.TransitionToWhite();
            }

            if (shootTime > 5f)
            {
                isShooting = false;
            }

            if (isReloading)
            {
                HUDScript.Instance.TransitionToWhite();
                reloadingImage.gameObject.SetActive(true);
                reloadingImage.fillAmount += Time.deltaTime;
                reloadTime += Time.deltaTime;
            }

            if (reloadTime > 1.5f)
            {
                int ammoNeeded = maxAmmo - currentAmmo;
                int ammoToReload = Mathf.Min(ammoNeeded, currentReserveAmmo);

                currentReserveAmmo -= ammoToReload;
                currentAmmo += ammoToReload;

                reloadTime = 0f;
                isReloading = false;
                reloadingImage.fillAmount = 0f;
                reloadingImage.gameObject.SetActive(false);
            }

            AmmoUI();
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
                // Realiza aqu� las acciones necesarias cuando se golpea a un enemigo
                // Por ejemplo, da�o al enemigo
                Destroy(hit.collider.gameObject); // Prueba
            }
        }

        currentAmmo--; // Reduce la munici�n activa despu�s de disparar
    }

    void AmmoUI()
    {
        ammoUI.text = currentAmmo.ToString() + "/" + currentReserveAmmo.ToString();
    }

    void Reload()
    {
        if (currentReserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            isReloading = true;
        }
    }
}