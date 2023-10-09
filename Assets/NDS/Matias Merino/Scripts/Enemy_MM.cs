using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_MM : MonoBehaviour
{
    private NavMeshAgent enemy_MM;
    private Transform playerTarget;
    public float chaseDistance = 10f; // La distancia máxima para perseguir al jugador.

    // Start is called before the first frame update
    void Start()
    {
        enemy_MM = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            // Si la distancia al jugador es menor que la distancia de persecución, establece la dirección de destino.
            if (distanceToPlayer <= chaseDistance)
            {
                enemy_MM.SetDestination(playerTarget.position);
            }
            else
            {
                // Si la distancia es mayor que la distancia de persecución, el enemigo se detiene.
                enemy_MM.ResetPath();
            }
        }
    }
}
