using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public CanvasGroup deathPanel;
    public CanvasGroup optionsPanel;
    public CanvasGroup hudPanel;
    public CanvasGroup quitScreen;
    public CanvasGroup darkScreen;
    public VolumeProfile volumeProfile;

    public GameObject deathText;
    public static bool isDead;
    private float optionsAppear;

    private bool isRestart = false;
    private float timeToRestart;
    private bool isBackToMenu = false;
    private float timeToMenu;

    private DepthOfField depthOfField;

    private void Awake()
    {
        isDead = false;
    }

    void Update()
    {
        if (isDead)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

            deathPanel.alpha += Time.unscaledDeltaTime * 1.5f;
            deathPanel.interactable = true;
            deathPanel.blocksRaycasts = true;

            hudPanel.alpha -= Time.unscaledDeltaTime * 4;
            hudPanel.interactable = false;
            hudPanel.blocksRaycasts = false;

            optionsAppear += Time.unscaledDeltaTime;

            if (volumeProfile.TryGet(out depthOfField))
            {
                depthOfField.focalLength.value += Time.unscaledDeltaTime * 3;

                if(depthOfField.focalLength.value >= 20)
                {
                    depthOfField.focalLength.value = 20;
                }
            }

            if (deathPanel.alpha == 1f)
            {
                deathPanel.alpha = 1f;

                deathText.SetActive(true);
            }

            if(optionsAppear >= 2.5f)
            {
                optionsPanel.alpha += Time.unscaledDeltaTime;
                optionsPanel.interactable = true;
            }

            if(isRestart)
            {
                timeToRestart += Time.unscaledDeltaTime;
                optionsPanel.alpha -= Time.unscaledDeltaTime * 2;
                darkScreen.alpha += Time.unscaledDeltaTime;

                if(timeToRestart >= 3)
                {
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);
                }
            }

            if (isBackToMenu)
            {
                darkScreen.alpha += Time.unscaledDeltaTime;
                hudPanel.alpha -= Time.unscaledDeltaTime * 1.3f;
                quitScreen.gameObject.SetActive(false);
                timeToMenu += Time.unscaledDeltaTime;

                if (timeToMenu >= 3)
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    public void Restart()
    {
        isRestart = true;
    }

    public void MenuOption()
    {
        quitScreen.gameObject.SetActive(true);
        optionsPanel.gameObject.SetActive(false);
        quitScreen.interactable = true;
    }

    public void BackToMenu()
    {
        isBackToMenu = true;
    }

    public void CancelExit()
    {
        quitScreen.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
        quitScreen.interactable = false;
    }

    private void OnDisable()
    {
            depthOfField.focalLength.value = 0;
    }
}
