using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    private InputsMap inputs;
    private AudioPlay AudioPlay;
    public Animator AnimatorGun;
    public int maxAmmo = 10; // Cantidad máxima de munición activa
    private int currentAmmo; // Munición activa actual

    public int currentReserveAmmo; // Munición de reserva actual

    private float reloadTime; // Tiempo de recarga en segundos
    private bool isReloading = false; // Está recargando

    private float shootTime; // Tiempo de recarga en segundos
    private bool isShooting = false;

    public TextMeshProUGUI ammoUI;
    public Image reloadingImage;
    public GameObject reloadingAdviser;
    public GameObject noAmmoAdviser;

    public GameObject particle_shoot_light, shoot_light;

    // Start is called before the first frame update
    void Start()
    {
        AudioPlay = GetComponent<AudioPlay>() ;
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
        if (Time.timeScale == 1) // Si el juego no está pausado
        {
            shootTime += Time.deltaTime;

            // Detecta el disparo
            if (inputs.Gameplay.Shoot.WasPressedThisFrame() && currentAmmo > 0 && !isReloading && shootTime > 0.5f)
            {
                Shoot();
                AudioPlay.ShootingGun();
                AnimatorGun.SetTrigger("Shoot");
                isShooting = true;
                shootTime = 0;
            }

            // Detecta la recarga
            if (inputs.Gameplay.Reload.WasPressedThisFrame())
            {
                Reload();
            }

            if (isShooting)
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

            if(currentAmmo <= 0 && currentReserveAmmo >= 1)
            {
                if(!isReloading)
                {
                    reloadingAdviser.SetActive(true);
                }
            }

            if(currentReserveAmmo <= 0 && currentAmmo <= 0)
            {
                noAmmoAdviser.SetActive(true);
            }
            else
            {
                noAmmoAdviser.SetActive(false);
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
                // Realiza aquí las acciones necesarias cuando se golpea a un enemigo
                // Por ejemplo, daño al enemigo
                //Destroy(hit.collider.gameObject); // Prueba
                hit.collider.GetComponent<Enemy_MM>().TakeDamage();
            }
        }
        StartCoroutine("shoot_lights");
        currentAmmo--; // Reduce la munición activa después de disparar
    }
    
    IEnumerator shoot_lights()
    {
        shoot_light.SetActive(true);
        particle_shoot_light.SetActive(true);
        yield return new WaitForSeconds(.1f);
        shoot_light.SetActive(false);
        particle_shoot_light.SetActive(false);
        yield return null;
    }

    void AmmoUI()
    {
        ammoUI.text = currentAmmo.ToString() + "/" + currentReserveAmmo.ToString();
    }

    void Reload()
    {
        if (currentReserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            reloadingAdviser.SetActive(false);
            isReloading = true;
            if (reloadTime<0.1f)
            {

                AnimatorGun.SetTrigger("Reload");
            }
            
        }
    }
}