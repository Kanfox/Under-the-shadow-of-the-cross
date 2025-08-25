using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;          // Velocidade de movimento
    public float jumpForce = 7f;      // Força do pulo

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Armazena a escala original
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Movimento horizontal
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Virar personagem horizontalmente sem alterar altura/largura original
        if (move < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (move > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Pulo (apenas uma vez ao pressionar a tecla)
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reseta velocidade vertical
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // ✅ Correção aplicada aqui:
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isRunning", Mathf.Abs(move) > 0 && isGrounded);
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