using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    CharacterController characterController;
    private InputsMap inputs;
    private Rigidbody rb;

    [Header("Movement")]
    private float walkSpeed = 6.0f;
    private float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float crouchwalkSpeed = 3f;
    public float crouchrunSpeed = 5.5f;
    public float crouchYscale;
    private float startYscale;
    private bool isCrouching;
    public float gravity = 20.0f;

    public Camera cam;
    private float mouseHorizontal = 3.0f;
    private float mouseVertical = 2.0f;


    private float minRotation = -65.0f;
    private float maxRotation = 60.0f;
    float h_mouse, v_mouse;


    private Vector3 move = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        startYscale = transform.localScale.y;
        isCrouching = false;

    }

    void Update()
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
            if(inputs.Gameplay.Crouch.WasPressedThisFrame() && !isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
                rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
                isCrouching = true;

                walkSpeed = crouchwalkSpeed;
                runSpeed = crouchrunSpeed;
            }
            else if (inputs.Gameplay.Crouch.WasPressedThisFrame() && isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
                isCrouching = false;

                walkSpeed = 6f;
                runSpeed = 10f;
            }
        }
        move.y -= gravity * Time.deltaTime;

        characterController.Move(move * Time.deltaTime);
    }
}
