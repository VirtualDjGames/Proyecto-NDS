using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    CharacterController characterController;
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
    private bool isCrouching;
    public float gravity = 20.0f;

    public Camera cam;
    private float mouseHorizontal = 3.0f;
    private float mouseVertical = 2.0f;


    private float minRotation = -65.0f;
    private float maxRotation = 60.0f;
    float h_mouse, v_mouse;


    public static Vector3 move = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        isCrouching = false;

    }

    void Update()
    {
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
                }
                else
                {
                    move = transform.TransformDirection(move) * walkSpeed;
                }

                //Jumping - Saltando
                if (inputs.Gameplay.Jump.WasPressedThisFrame())
                {
                    move.y = jumpSpeed;
                }

                //Crouching - Agacharse
                if (inputs.Gameplay.Crouch.WasPressedThisFrame() && !isCrouching)
                {
                    characterController.height = 1.1f;
                    isCrouching = true;

                    walkSpeed = crouchwalkSpeed;
                    runSpeed = crouchrunSpeed;
                }
                else if (inputs.Gameplay.Crouch.WasPressedThisFrame() && isCrouching)
                {
                    characterController.height = 2f;
                    isCrouching = false;

                    walkSpeed = 6f;
                    runSpeed = 10f;
                }
            }
            move.y -= gravity * Time.deltaTime;

            characterController.Move(move * Time.deltaTime);

            HpFeedbackUI();
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
        }
    }
}
