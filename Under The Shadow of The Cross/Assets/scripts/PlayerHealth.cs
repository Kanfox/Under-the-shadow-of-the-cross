using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Se j� morreu, n�o toma mais dano

        currentHealth -= damage;
        Debug.Log("Player tomou dano! Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("Player morreu!");

        // Ativa a anima��o de morte
        animator.SetBool("isDead", true);

        // Desativa o controle do jogador (substitua pelo nome do seu script de movimento)
        PlayerController movementScript = GetComponent<PlayerController>(); // <--- troque aqui se for outro nome
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // Voc� pode adicionar outras l�gicas aqui, como reiniciar fase, mostrar UI, etc.
    }
}
