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

    public float distancia_ataque;
    public float radio_vision;

    public NavMeshAgent agente;

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
            cronometro += 1 * Time.deltaTime;
            if (cronometro>=4)
            {
                rutina = Random.Range(0,3);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0: //invocar enemigos
                    anim.SetInteger("attack",1);
                    break;
                case 1: // bola de fuego
                    anim.SetInteger("attack", 2);
                    break;
                case 2: // charco de fuego
                    anim.SetInteger("attack", 3);
                    break;
            }
        }
        else
        {
            anim.SetInteger("attack", 0);
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            agente.enabled = true;
            agente.SetDestination(target.transform.position);

            
            if (Vector3.Distance(transform.position, target.transform.position)> distancia_ataque && !atacando)
            {
                //animacion de caminar true

            }

        }
        if (atacando)
        {
           
            agente.enabled = false;
        }
    }
}
