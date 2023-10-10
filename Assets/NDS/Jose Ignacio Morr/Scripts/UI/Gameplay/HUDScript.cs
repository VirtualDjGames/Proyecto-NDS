using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    private static HUDScript instance;

    public Image hpedge;
    public Image[] hpImages;
    public TextMeshProUGUI ammoTxt;

    // Valor de la transparencia deseada (0 para completamente transparente, 1 para completamente opaco)
    private float targetAlphas = 2.0f;
    // Velocidad de la transición de alpha
    public float alphaTransitionSpeed = 0.5f;

    public static HUDScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HUDScript>();
            }
            return instance;
        }
    }

    public void TransitionToWhite()
    {
        // Lerp para cambiar gradualmente el alpha hacia el valor objetivo
        float currentAlpha = Mathf.Lerp(hpedge.color.a, targetAlphas, Time.deltaTime * alphaTransitionSpeed);

        // Actualiza el color y la transparencia
        SetColorAndAlpha(Color.white, currentAlpha);
    }

    public void TransitionToGray()
    {
        // Color gris (R, G, B en 171/255 para obtener el valor normalizado)
        Color grayColor = new Color(171f, 171f, 171f);

        // Establece el valor de alfa al 16/255 para normalizarlo a un rango de 0 a 1
        float targetAlpha = 0.01f;

        // Lerp para cambiar gradualmente el alpha hacia el valor objetivo
        float currentAlpha = Mathf.Lerp(hpedge.color.a, targetAlpha, Time.deltaTime * alphaTransitionSpeed);

        // Actualiza el color y la transparencia
        SetColorAndAlpha(grayColor, currentAlpha);
    }



    // Función para configurar el color y la transparencia de las imágenes y el texto
    private void SetColorAndAlpha(Color color, float alpha)
    {
        // Configura el color y la transparencia para hpedge
        hpedge.color = new Color(color.r, color.g, color.b, alpha);

        // Configura el color y la transparencia para hpImages
        Color hpImagesColor = new Color(color.r, color.g, color.b, alpha);
        hpImages[0].color = hpImagesColor;
        hpImages[1].color = hpImagesColor;
        hpImages[2].color = hpImagesColor;
        hpImages[3].color = hpImagesColor;

        // Configura el color y la transparencia para ammoTxt
        Color ammoTxtColor = new Color(color.r, color.g, color.b, alpha);
        ammoTxt.color = ammoTxtColor;
    }
}

