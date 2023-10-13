using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private float timeToPress;
    private float timeToGame;
    private bool isMenu = false;
    private bool isTimeToPlay = false;
    public CanvasGroup introScreen;
    public CanvasGroup menuScreen;
    public CanvasGroup quitScreen;
    public CanvasGroup darkScreen;
    public GameObject settingsWindow;
    public GameObject creditsWindow;
    public Animator introAnim;
    public AudioClip[] Menu;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        //Intro
        if (!isMenu)
        {
            timeToPress += Time.deltaTime;
        }
        else
        {
            if (!isTimeToPlay)
            {
                introScreen.alpha -= Time.deltaTime * 5;
                introScreen.interactable = false;

                menuScreen.alpha += Time.deltaTime * 2;
                menuScreen.interactable = true;
            }
        }

        if (timeToPress >= 5)
        {
            timeToPress = 5.1f;
            introAnim.SetBool("TextTime", true);

            if (Input.anyKey && !isMenu)
            {
                isMenu = true;
                timeToPress = 0;
            }
        }

        //ToGameplay
        if (isTimeToPlay)
        {
            darkScreen.alpha += Time.deltaTime;
            menuScreen.alpha -= Time.deltaTime * 1.3f;
            menuScreen.interactable = false;
            timeToGame += Time.deltaTime;            
            float newVolume = Mathf.Lerp(1, 0.0001f, timeToGame / 3.2f);
            AudioManager.Instance.audioSourceMusic.volume = newVolume;
            

            if (timeToGame >= 3.2f)
            {
                SceneManager.LoadScene("Nivel 1");
            }
        }
    }

    public void StartPlay()
    {
        isTimeToPlay = true;
        AudioManager.Instance.PlayGlobalSoundEffect(Menu[0], 2);
        settingsWindow.SetActive(false);
        creditsWindow.SetActive(false);
    }

    public void QuitOption()
    {
        quitScreen.gameObject.SetActive(true);
        settingsWindow.SetActive(false);
        creditsWindow.SetActive(false);

        quitScreen.interactable = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        quitScreen.gameObject.SetActive(false);
        quitScreen.interactable = false;
    }

    public void SettingsOpen()
    {
        settingsWindow.SetActive(true);
        creditsWindow.SetActive(false);
    }
    public void SettingsClosed()
    {
        settingsWindow.SetActive(false);
    }

    public void CreditsOpen()
    {
        creditsWindow.SetActive(true);
        settingsWindow.SetActive(false);
    }
    public void CreditsClosed()
    {
        creditsWindow.SetActive(false);
    }
}
