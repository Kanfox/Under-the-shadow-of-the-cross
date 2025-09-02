using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 3f;
    public float attackRange = 2f;
    public float damage = 10f;
    public float attackCooldown = 2f;
    private float LastAttackTime;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    private bool isGrounded; // Variável para detectar se está no chão

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Verifica se está no chão antes de permitir movimentação
        if (!isGrounded)
        {
            // Pode colocar animação de queda ou parar movimentação aqui, se quiser
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Atacar
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);

            if (Time.time > LastAttackTime + attackCooldown)
            {
                animator.SetBool("isAttacking", true);
                AttackPlayer();
                LastAttackTime = Time.time;
            }
        }
        else if (distance <= detectionRange)
        {
            // Perseguir
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", false);
        }
        else
        {
            // Parado (Idle)
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdle", true);
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    // Detecta colisão com chão usando tag "chao"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            isGrounded = false;
        }
    }
}