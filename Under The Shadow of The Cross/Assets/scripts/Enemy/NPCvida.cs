using UnityEngine;
using System.Collections;

public class NPCVida : MonoBehaviour
{
    public int health = 20;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;
        Debug.Log("NPC took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            rb.simulated = false;
        }

        // Destrói o objeto após 10 segundos
        StartCoroutine(DestroyAfterDelay(10f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
