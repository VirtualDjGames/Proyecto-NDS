using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_MM : MonoBehaviour
{
    private NavMeshAgent enemy_MM;
    private Transform playerTarget;
    private Animator enemyAnimator;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    private bool isAttacking = false;
    public int vida = 3;
    private Collider col;
    GameObject mano;
     

    // Start is called before the first frame update
    void Start()
    {
        enemy_MM = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag("Player").transform;
        enemyAnimator = GetComponent<Animator>();
        col = GetComponent<Collider>();
        mano = transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;

        if (playerTarget == null)
        {
            Debug.LogError("No se encontr� un objeto con la etiqueta 'Player'. Aseg�rate de que el jugador tiene la etiqueta 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null && vida > 0)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            if (distanceToPlayer <= chaseDistance && isAttacking == false)
            {
                // Si la distancia al jugador es menor que la distancia de persecuci�n, establece la direcci�n de destino.
                enemy_MM.SetDestination(playerTarget.position);

                // Configura la animaci�n de caminar.
                enemyAnimator.SetBool("IsWalking", true);
            }
            else
            {
                // Si la distancia es mayor que la distancia de persecuci�n, el enemigo se detiene y configura la animaci�n de idle.
                enemy_MM.ResetPath();
                enemyAnimator.SetBool("IsWalking", false);
            }
            // Verifica si el enemigo est� lo suficientemente cerca para atacar.
            if (distanceToPlayer <= attackDistance)
            {
                // Configura la animaci�n de ataque y realiza la l�gica de ataque.
                enemy_MM.ResetPath();
                enemyAnimator.SetBool("IsAttacking", true);
                Attack();
            }
        }

        if (vida <= 0)
        {
            enemyAnimator.SetBool("IsDead", true);
            col.enabled = false;
            mano.SetActive(false);
            enemy_MM.ResetPath();
        }
    }

    public void EndAttackAnimation()
    {
        enemyAnimator.SetBool("IsAttacking", true);
    }

    public void TakeDamage()
    {
        vida--;
        //enemyAnimator.SetBool("IsDamaged",true);
    }

    // L�gica para el ataque (puedes personalizar esto).
    void Attack()
    {
        if (!isAttacking)
        {
            transform.LookAt(new Vector3(playerTarget.transform.position.x,transform.position.y,playerTarget.transform.position.z));
        }
    }
}
