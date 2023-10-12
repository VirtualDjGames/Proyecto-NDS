using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    private InputsMap inputs;

    //public AudioSource AudioFlash;
    public bool LightActive;
    public GameObject[] Lights;
    public GameObject flashlightAdviser;
    
    void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        Lights[0].SetActive(false);
        Lights[1].SetActive(false);
    }

    void Update()
    {
        if (inputs.Gameplay.Flashlight.WasPressedThisFrame())
        {
            flashlightAdviser.SetActive(false);

            LightActive = !LightActive;
            if (LightActive)
            {
                flashlightActive();
                //AudioFlash.Play();
            }
            if (!LightActive)
            {
                flashlightInactive();
                //AudioFlash.Play();
            }
        }
    }
    void flashlightActive()
    {
        Lights[0].SetActive(true);
        Lights[1].SetActive(true);
    }
    void flashlightInactive()
    {
        Lights[0].SetActive(false);
        Lights[1].SetActive(false);
    }


}
