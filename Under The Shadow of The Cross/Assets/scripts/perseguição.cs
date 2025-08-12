using UnityEngine;
using UnityEngine.AI; // Importa NavMesh

public class EnemyPerception : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f;
    public float viewAngle = 45f;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private bool playerDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            playerDetected = true;
            Debug.Log("Jogador detectado! Correndo atrás...");
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            // Define o destino do agente para a posição do jogador
            agent.SetDestination(player.position);
        }
        else
        {
            // Para o inimigo se não detectar o jogador
            agent.ResetPath();
        }
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < viewDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < viewAngle)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
