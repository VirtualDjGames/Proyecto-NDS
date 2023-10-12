using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyDoorManager: MonoBehaviour
{
    public GameObject keyAddedAdviser;
    public GameObject keyRemovedAdviser;
    public GameObject grabKeyAdviser;
    public GameObject openDoorAdviser;
    public GameObject hasNoKeyAdviser;

    private InputsMap inputs;
    private bool isInteractuableKey = false;
    private bool isInteractuableDoor = false;
    private bool isInteractuableDoorNoKey = false;
    private List<string> keys = new List<string>(); //Llaves

    

    //Agrega una llave al inventario
    public void AddKey(string keyName)
    {
        keys.Add(keyName);
        keyAddedAdviser.SetActive(true);
        keyRemovedAdviser.SetActive(false);
    }

    //Remueve una llave del inventario
    public void RemoveKey(string keyName)
    {
        keys.Remove(keyName);
        keyAddedAdviser.SetActive(false);
        keyRemovedAdviser.SetActive(true);
    }

    //Verifica si una llave está en el inventario
    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    private void Start()
    {
        inputs = new InputsMap();
        inputs.Gameplay.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            grabKeyAdviser.SetActive(true);
        }

        if (other.gameObject.tag == "Door" || other.gameObject.tag == "DoorNoKey")
        {
            openDoorAdviser.SetActive(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Interacción con llaves
        if (other.gameObject.tag == "Key")
        {
            isInteractuableKey = true;

            if (isInteractuableKey)
            {
                if (inputs.Gameplay.Interaction.WasPressedThisFrame())
                {
                    AddKey("Ejemplo");
                    grabKeyAdviser.SetActive(false);
                    Destroy(other.gameObject);
                }
            }
        }
        else
        {
            isInteractuableKey = false;
        }


        //Interacción con puertas
        if (other.gameObject.tag == "Door")
        {
            isInteractuableDoor = true;

            if (isInteractuableDoor)
            {
                if (inputs.Gameplay.Interaction.WasPressedThisFrame())
                {
                    if (HasKey("Ejemplo"))
                    {
                        other.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        Destroy(other.GetComponent<BoxCollider>());
                        openDoorAdviser.SetActive(false);
                        RemoveKey("Ejemplo"); // Elimina la llave después de usarla
                    }
                    else
                    {
                        //Feedback: No tienes la llave
                        openDoorAdviser.SetActive(false);
                        hasNoKeyAdviser.SetActive(true);
                    }
                }
            }
        }
        else
        {
            isInteractuableDoor = false;
        }

        //Interacción con puertas sin llaves
        if(other.gameObject.tag == "DoorNoKey")
        {
            isInteractuableDoorNoKey = true;

            if (isInteractuableDoorNoKey)
            {
                if (inputs.Gameplay.Interaction.WasPressedThisFrame())
                {
                        other.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        Destroy(other.GetComponent<BoxCollider>());
                        openDoorAdviser.SetActive(false);
                }
            }
        }
        else
        {
            isInteractuableDoorNoKey = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grabKeyAdviser.SetActive(false);
        openDoorAdviser.SetActive(false);
        hasNoKeyAdviser.SetActive(false);

    }
}
