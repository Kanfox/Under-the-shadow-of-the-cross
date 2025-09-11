using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public Animator animator;

    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Se jogador est� no alcance, n�o est� atacando, e cooldown acabou
        if (distance <= attackRange && !isAttacking && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack());
        }
    }

    private System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Inicia anima��o de ataque
        animator.SetTrigger("Attack");

        // Espera at� o fim da anima��o para permitir novo ataque
        // Voc� pode ajustar isso para tempo fixo se preferir
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }
}
