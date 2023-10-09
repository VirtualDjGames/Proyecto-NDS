using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo_1 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator anim;

    public GameObject target;
    public bool atacando;
    public int ataque;

    public float distancia_ataque;
    public float radio_vision;

    public NavMeshAgent agente;

    public GameObject bola_de_fuego,charco_de_fuego,invocacion;
    public Transform[] spawn;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= distancia_ataque && !atacando)
        {
            agente.enabled = false;
            // rutina = Random.Range(0, 3);
            rutina = 0;
            atacando = true;
            switch (rutina)
            {
                case 0: //bola de fuegoa
                    StartCoroutine("ataque1"); //listo
                    break;
                case 1: // charco de fuego
                    StartCoroutine("ataque2");//listo
                    break;
                case 2: // invocar
                    StartCoroutine("ataque3");//en proceso
                    break;
            }
        }
        else
        {
            
            var lookPos = target.transform.position - transform.position;

            if (!atacando)
            {
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            }
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            agente.enabled = true;
            agente.SetDestination(target.transform.position);

        }
        if (atacando)
        {
            cronometro += 1 * Time.deltaTime;
            agente.enabled = false;
            if (cronometro >= 5)
            {
                atacando = false;
                cronometro = 0;
            }
        }
    }
    IEnumerator ataque1()//bola de fuego
    {
        ataque = 1;
        anim.SetInteger("attack", ataque);
        Instantiate(bola_de_fuego, spawn[0].transform.position, transform.rotation);
        yield return new WaitForSecondsRealtime(.2f);
        anim.SetInteger("attack", 0);
        yield return null;
    }
    IEnumerator ataque2()//charco de fuego
    {
        ataque = 2;
        anim.SetInteger("attack", ataque);
        yield return new WaitForSecondsRealtime(.2f);
        anim.SetInteger("attack", 0);
        yield return new WaitForSecondsRealtime(.8f);
        Instantiate(charco_de_fuego, spawn[1].transform.position, transform.rotation);
        yield return null;
    }
    IEnumerator ataque3()//invocacion
    {
        ataque = 3;
        anim.SetInteger("attack", ataque);
        Instantiate(invocacion, spawn[2].transform.position, transform.rotation);
        yield return null;
    }
}
