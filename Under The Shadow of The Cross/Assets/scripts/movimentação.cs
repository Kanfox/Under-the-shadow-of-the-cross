using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;          // Velocidade de movimento
    public float jumpForce = 7f;      // Força do pulo
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;   // Verifica se está no chão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Define o tamanho inicial do personagem
        transform.localScale = new Vector3(1.561f, 2.0916f, 1f);
    }

    void Update()
    {
        // Movimento horizontal
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Virar personagem para esquerda/direita
        if (move > 0)
            transform.localScale = new Vector3(1.561f, 2.0916f, 1f);
        else if (move < 0)
            transform.localScale = new Vector3(-1.561f, 2.0916f, 1f);

        // Pular enquanto segura Space ou W
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reseta velocidade vertical
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Animações
        if (!isGrounded)
        {
            animator.Play("Jump"); // animação de pulo
        }
        else if (move != 0)
        {
            animator.Play("Run"); // animação de correr
        }
        else
        {
            animator.Play("Idle"); // animação parado
        }
    }

    // Detecta colisão com o chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }

    }

}