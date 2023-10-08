using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Intro
        if(!isMenu)
        {
            timeToPress += Time.deltaTime;
        }
        else
        {
            if(!isTimeToPlay)
            {
                introScreen.alpha -= Time.deltaTime * 5;
                introScreen.interactable = false;

                menuScreen.alpha += Time.deltaTime * 2;
                menuScreen.interactable = true;
            }
        }

        if(timeToPress >= 5) 
        { 
            timeToPress = 5.1f;

          if(Input.anyKey && !isMenu)
          {
                isMenu = true;
                timeToPress = 0;
          }
        }

        if(isTimeToPlay) 
        {
            menuScreen.alpha -= Time.deltaTime * 1.2f;
            menuScreen.interactable = false;
            timeToGame += Time.deltaTime;

            if(timeToGame >= 2)
            {
                SceneManager.LoadScene("Nivel 1");
            }    
        }
    }

    public void StartPlay()
    {
        isTimeToPlay = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
