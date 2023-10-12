using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyDoorManager: MonoBehaviour
{
    private InputsMap inputs;
    private bool isInteractuableKey = false;
    private bool isInteractuableDoor = false;
    private List<string> keys = new List<string>(); //Llaves

    

    //Agrega una llave al inventario
    public void AddKey(string keyName)
    {
        keys.Add(keyName);
    }

    //Remueve una llave del inventario
    public void RemoveKey(string keyName)
    {
        keys.Remove(keyName);
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

    private void OnTriggerStay(Collider other)
    {
        //Interacción con llaves
        if (other.gameObject.tag == "Key")
        {
            isInteractuableKey = true;
            //Aparece el aviso

            if (isInteractuableKey)
            {
                if (inputs.Gameplay.Interaction.WasPressedThisFrame())
                {
                    AddKey("Ejemplo");
                    Debug.Log("Añadiste la llave Ejemplo");
                    Destroy(other.gameObject);
                }
            }
        }
        else
        {
            isInteractuableKey = false;
            //Desaparece el aviso
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
                        // Aquí puedes realizar acciones específicas al usar la llave, como abrir una puerta, etc.
                        Debug.Log("Usaste la llave Ejemplo");
                        other.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                        RemoveKey("Ejemplo"); // Elimina la llave después de usarla
                        //Se mueve la puerta
                    }
                    else
                    {
                        //Feedback: No tienes la llave
                    }
                }
            }
        }
        else
        {
            isInteractuableDoor = false;
        }
    }
}
