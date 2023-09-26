
using UnityEngine;


public class Flashlight : MonoBehaviour
    
{
    public AudioSource AudioFlash;
    public bool LightActive;
    public GameObject Light;
    
    void Start()
    {
        Light.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightActive = !LightActive;
            if (LightActive)
            {
                flashlightActive();
                AudioFlash.Play();
            }
            if (!LightActive)
            {
                flashlightInactive();
                AudioFlash.Play();
            }
        }
    }
    void flashlightActive()
    {
        Light.SetActive(true);
    }
    void flashlightInactive()
    {
        Light.SetActive(false);
    }


}
