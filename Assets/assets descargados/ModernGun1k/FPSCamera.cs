using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    CharacterController characterController;
    private CharacterController controller;
    public Camera fpsCamera;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float speed = 2f;
    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;

    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        {
            //Vision
            h = horizontalSpeed * Input.GetAxis("Mouse X");
            v += verticalSpeed * Input.GetAxis("Mouse Y");

            v = Mathf.Clamp(v, minRotation, maxRotation);


            fpsCamera.transform.localEulerAngles = new Vector3(-v, 0, 0);

            transform.Rotate(0, h, 0);
        }
        //Movimiento
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
        }

       
    }
}
