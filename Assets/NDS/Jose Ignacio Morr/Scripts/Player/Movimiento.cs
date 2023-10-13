using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    CharacterController characterController;
    private AudioPlay AudioPlay;
    private InputsMap inputs;
    private Rigidbody rb;

    [Header("Health")]
    public int HP;
    public Image[] hpImages;
    public static bool isAttack = false;
    public float timeAttack;

    [Header("Movement")]
    private float walkSpeed = 6.0f;
    private float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float crouchwalkSpeed = 3f;
    public float crouchrunSpeed = 5.5f;
    public static bool isCrouching, isRunning, isJump;
    public float gravity = 20.0f;

    public Camera cam;
    private float mouseHorizontal = 3.0f;
    private float mouseVertical = 2.0f;

    private float minRotation = -65.0f;
    private float maxRotation = 60.0f;
    float h_mouse, v_mouse;

    public VolumeProfile volumeProfile;
    private Vignette vignetteHP;

    public float cronometro;

    public CanvasGroup darkScreen;

    public static Vector3 move = Vector3.zero;
    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        AudioPlay = GetComponent<AudioPlay>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        isAttack = false;
        isCrouching = false;

    }

    void Update()
    {
        cronometro += Time.deltaTime;

        if(Time.timeScale == 1) //Si el juego no esta pausado
        {
            h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
            v_mouse += mouseVertical * Input.GetAxis("Mouse Y");

            v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);


            cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);

            transform.Rotate(0, h_mouse, 0);


            if (characterController.isGrounded)
            {
                //Moving - Moverse
                move = inputs.Gameplay.Walk.ReadValue<Vector3>();

                //Running - Corriendo
                if (inputs.Gameplay.Run.IsPressed())
                {
                    move = transform.TransformDirection(move) * runSpeed;
                    isRunning = true;
                }
                else
                {
                    isRunning = false;
                    move = transform.TransformDirection(move) * walkSpeed;
                }

                //Jumping - Saltando
                if (inputs.Gameplay.Jump.WasPressedThisFrame())
                {
                    move.y = jumpSpeed;
                    isJump = true;
                }              

                //Crouching - Agacharse
                if (inputs.Gameplay.Crouch.WasPressedThisFrame() && !isCrouching)
                {
                    characterController.height = 1.5f;
                    isCrouching = true;
                    AudioPlay.pitch = 0.5f;
                   

                    walkSpeed = crouchwalkSpeed;
                    runSpeed = crouchrunSpeed;
                }
                else if (inputs.Gameplay.Crouch.WasPressedThisFrame() && isCrouching)
                {
                    characterController.height = 3.05f;
                    isCrouching = false;
                    

                    walkSpeed = 6f;
                    runSpeed = 10f;
                }
            }
            move.y -= gravity * Time.deltaTime;

            characterController.Move(move * Time.deltaTime);

            if (HP <= 0)
            {
                if(!Enemigo_1.activacion)
                {
                    DeathScript.isDead = true;
                }

                darkScreen.alpha += Time.unscaledDeltaTime * 0.7f;

                if (darkScreen.alpha >= 1)
                {
                    SceneManager.LoadScene("Ending");
                }
            }


            HpFeedbackUI();
            HpFeedbackVignette();
            HUDScript.Instance.TransitionToGray();

            if (isAttack == true)
            {
                HUDScript.Instance.TransitionToWhite();
                timeAttack += Time.deltaTime;
            }

            if(timeAttack >= 5f)
            {
                isAttack = false;
                timeAttack = 0f;
            }
        }
    }

    void HpFeedbackVignette()
    {
        if (volumeProfile.TryGet(out vignetteHP))
        {
            switch (HP)
            {
                case 4:
                    vignetteHP.intensity.value = 0f;
                    break;

                case 3:
                    vignetteHP.intensity.value += Time.deltaTime;
                    if (vignetteHP.intensity.value >= 0.28f)
                    {
                        vignetteHP.intensity.value = 0.28f;
                    }
                    break;

                case 2:
                    vignetteHP.intensity.value += Time.deltaTime * 0.3f;
                    if (vignetteHP.intensity.value >= 0.36f)
                    {
                        vignetteHP.intensity.value = 0.36f;
                    }
                    break;

                case 1:
                    vignetteHP.intensity.value += Time.deltaTime * 0.3f;
                    if (vignetteHP.intensity.value >= 0.41f)
                    {
                        vignetteHP.intensity.value = 0.41f;
                    }
                    break;

                case 0:
                        vignetteHP.intensity.value = 0.48f;
                    break;
            }
        }
    }
    
    void HpFeedbackUI()
    {
        switch(HP) 
        {
            case 4:
                hpImages[3].gameObject.SetActive(true);
                hpImages[2].gameObject.SetActive(true);
                hpImages[1].gameObject.SetActive(true);
                hpImages[0].gameObject.SetActive(true);
                break;

            case 3:
                hpImages[3].gameObject.SetActive(false);
                hpImages[2].gameObject.SetActive(true);
                hpImages[1].gameObject.SetActive(true);
                hpImages[0].gameObject.SetActive(true);
                break;

            case 2:
                hpImages[3].gameObject.SetActive(false);
                hpImages[2].gameObject.SetActive(false);
                hpImages[1].gameObject.SetActive(true);
                hpImages[0].gameObject.SetActive(true);
                break;

            case 1:
                hpImages[3].gameObject.SetActive(false);
                hpImages[2].gameObject.SetActive(false);
                hpImages[1].gameObject.SetActive(false);
                hpImages[0].gameObject.SetActive(true);
                break;

            case 0:
                hpImages[3].gameObject.SetActive(false);
                hpImages[2].gameObject.SetActive(false);
                hpImages[1].gameObject.SetActive(false);
                hpImages[0].gameObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HP--;
            isAttack = true;
            //Sonido de Daño
            if (HP != 0)
            {
                AudioPlay.DamageLive();
            }
        }
        if (other.gameObject.tag == "Bola_de_Fuego")
        {
            HP--;
            //Sonido de Daño
            if (HP != 0)
            {
                AudioPlay.DamageLive();
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Crucifijo"))
        {
            other.GetComponent<AutoMuteSFX3D>().Crujido();
            other.GetComponent<Animator>().SetBool("isRotated", true);
        }
        if (other.CompareTag("ViolinAMB"))
        {            
            AudioPlay.AmbientSound(2,2.6f);
        }
        if (other.CompareTag("Escalofrio"))
        {
            AudioPlay.AmbientSound(5, 1);
            AudioPlay.AmbientSound(1, 1);
        }

        if (other.CompareTag("MonsterSearch"))
        {
            other.gameObject.SetActive(false);
            AudioPlay.AmbientSound(4, 1.4f);
        }
        if (other.CompareTag("Suspenso"))
        {
            AudioPlay.AmbientSoundSuspenso(6, 2.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crucifijo"))
        {
            other.GetComponent<Animator>().SetBool("isRotated", false);
        }
        if (other.CompareTag("MonsterSearch") || other.CompareTag("ViolinAMB") ||
            (other.CompareTag("Escalofrio") || (other.CompareTag("Suspenso"))))
        {
            AudioManager.Instance.audioSourceAmbient.volume = 0.4f;
        }
        if (other.CompareTag("Suspenso"))
        {
            AudioPlay.AmbientSoundSuspenso(0, 0.4f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "charco_de_Fuego")
        {
            if (cronometro >= 3f)
            {
                HP--;
                //Sonido de Daño
                if (HP != 0)
                {
                    AudioPlay.DamageLive();
                }
                cronometro = 0;
            }
        }
        if (other.CompareTag("Violin") && !AudioManager.Instance.audioSourceAmbientShot.isPlaying)
        {
            AudioPlay.AmbientSound(2, 2.6f);
        }
        if (other.CompareTag("Suspenso"))
        {
            AudioPlay.AmbientSoundSuspenso(0, 2.5f);
        }
    }
}
