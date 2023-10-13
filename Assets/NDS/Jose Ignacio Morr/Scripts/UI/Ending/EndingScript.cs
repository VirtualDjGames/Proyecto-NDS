using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public GameObject finishAdviser;

    private float finishTime;
    private bool isFinish = false;
    void Start()
    {
        Time.timeScale = 1;
        Enemigo_1.activacion = false;
    }


    void Update()
    {
        finishTime += Time.deltaTime;

        if(finishTime >= 20f)
        { 
            isFinish = true;
        }

        if (isFinish)
        {
            finishAdviser.SetActive(true);

            if (Input.anyKey)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
