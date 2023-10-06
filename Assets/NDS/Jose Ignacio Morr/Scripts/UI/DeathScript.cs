using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CanvasGroup deathPanel;
    public CanvasGroup optionsPanel;
    public CanvasGroup hudPanel;
    public VolumeProfile volumeProfile;

    public GameObject deathText;
    public static bool isDead;
    private float optionsAppear;
    private DepthOfField depthOfField;

    private void Awake()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

            deathPanel.alpha += Time.unscaledDeltaTime * 3;
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

            if(optionsAppear >= 2f)
            {
                optionsPanel.alpha += Time.unscaledDeltaTime * 2;
                optionsPanel.interactable = true;
            }
        }
    }

    private void OnDisable()
    {
        depthOfField.focalLength.value = 0;
    }


}
