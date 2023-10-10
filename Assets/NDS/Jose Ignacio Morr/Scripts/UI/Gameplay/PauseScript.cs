using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private InputsMap inputs;
    public static bool isPaused = false;
    public CanvasGroup pausePanel;
    public CanvasGroup quitScreen;
    public CanvasGroup hudPanel;
    public CanvasGroup darkScreen;
    public GameObject settingsWindow;

    private bool isBackToMenu = false;
    private float timeToMenu;

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

            if (isBackToMenu)
            {
                darkScreen.alpha += Time.unscaledDeltaTime;
                hudPanel.alpha -= Time.unscaledDeltaTime * 1.3f;
                pausePanel.interactable = false;
                quitScreen.gameObject.SetActive(false);
                timeToMenu += Time.unscaledDeltaTime;

                if (timeToMenu >= 3)
                {
                    SceneManager.LoadScene("Menu");
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
        settingsWindow.SetActive(false);

        inputs.UI.Disable();
        inputs.Gameplay.Enable();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
    }

    public void SettingsOpen()
    {
        settingsWindow.SetActive(true);
    }

    public void SettingsClosed()
    {
        settingsWindow.SetActive(false);
    }

    public void MenuOption()
    {
        settingsWindow.SetActive(false);

        quitScreen.gameObject.SetActive(true);
        quitScreen.interactable = true;
    }

    public void BackToMenu()
    {
        isBackToMenu = true;
    }

    public void CancelExit()
    {
        quitScreen.gameObject.SetActive(false);
        quitScreen.interactable = false;
    }
}
