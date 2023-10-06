using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private InputsMap inputs;
    private bool isPaused = false;
    public CanvasGroup pausePanel;
    public CanvasGroup hudPanel;

    void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
        inputs.UI.Disable();
    }


    void Update()
    {
        if (!DeathScript.isDead)
        {
            if (inputs.Gameplay.Pause.WasPressedThisFrame())
            {
                Paused();
            }

            if (inputs.UI.Resume.WasPressedThisFrame())
            {
                Resume();
            }

            if (isPaused)
            {
                hudPanel.alpha -= Time.unscaledDeltaTime * 4;
                pausePanel.alpha += Time.unscaledDeltaTime * 4;
                hudPanel.interactable = false;
                pausePanel.interactable = true;

                if (hudPanel.alpha == 0f && pausePanel.alpha == 1f)
                {
                    hudPanel.alpha = 0f;
                    pausePanel.alpha = 1f;
                }
            }
            else
            {
                hudPanel.alpha += Time.unscaledDeltaTime * 4;
                pausePanel.alpha -= Time.unscaledDeltaTime * 4;
                hudPanel.interactable = true;
                pausePanel.interactable = false;

                if (hudPanel.alpha == 1f && pausePanel.alpha == 0f)
                {
                    hudPanel.alpha = 1f;
                    pausePanel.alpha = 0f;
                }
            }
        }
    }

    private void Paused()
    {
        inputs.Gameplay.Disable();
        inputs.UI.Enable();
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }    

    public void Resume()
    {
        inputs.UI.Disable();
        inputs.Gameplay.Enable();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
    }
}
