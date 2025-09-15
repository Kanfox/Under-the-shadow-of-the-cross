using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar cenas
using System.Collections; // Necess�rio para usar IEnumerator

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    public bool isDead = false;

    [Header("Painel de Game Over")]
    public GameObject gameOverPanel; // Painel a ser ativado

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // Garante que o painel esteja escondido no come�o
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

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

        // Desativa o controle do jogador
        PlayerController movementScript = GetComponent<PlayerController>();
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // Inicia corrotina para mostrar o painel com delay
        StartCoroutine(ShowGameOverPanelWithDelay(1.5f)); // Delay de 1.5 segundos (ajust�vel)
    }

    private IEnumerator ShowGameOverPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // Chamado pelo bot�o "Reiniciar"
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Chamado pelo bot�o "Sair para Menu"
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); // Certifique-se que a cena "Menu" est� adicionada no Build Settings
    }
}
