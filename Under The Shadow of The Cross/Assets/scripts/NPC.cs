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

    private bool isGrounded; // Vari�vel para detectar se est� no ch�o

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Verifica se est� no ch�o antes de permitir movimenta��o
        if (!isGrounded)
        {
            // Pode colocar anima��o de queda ou parar movimenta��o aqui, se quiser
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

    // Detecta colis�o com ch�o usando tag "chao"
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