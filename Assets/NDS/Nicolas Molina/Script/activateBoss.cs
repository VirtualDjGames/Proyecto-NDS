using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoss : MonoBehaviour
{
    public GameObject boss, ubicacion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(boss, ubicacion.transform.position, transform.rotation);

            Enemigo_1.activacion = true;

            gameObject.SetActive(false);
        }
        
    }
}
