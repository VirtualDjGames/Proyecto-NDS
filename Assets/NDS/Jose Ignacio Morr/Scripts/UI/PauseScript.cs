using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private InputsMap inputs;

    void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        inputs.UI.Disable();
    }


    void Update()
    {
        if (inputs.Gameplay.Pause.WasPressedThisFrame())
        {
            inputs.Gameplay.Disable();
            inputs.UI.Enable();
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }

        if (inputs.UI.Resume.WasPressedThisFrame())
        {
            inputs.UI.Disable();
            inputs.Gameplay.Enable();
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }
}
