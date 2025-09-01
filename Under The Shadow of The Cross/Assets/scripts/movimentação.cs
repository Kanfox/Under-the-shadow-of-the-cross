using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float mov = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(mov * speed, rb.linearVelocity.y);

        // Virar personagem
        if (mov > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (mov < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        // Pulo
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Atualizar animações movimento (exemplo seu, pode variar)
        bool isJumping = !isGrounded;
        bool isRunning = Mathf.Abs(mov) > 0 && isGrounded;
        bool isIdle = mov == 0 && isGrounded;

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);

        // ** ATAQUES ADICIONADOS SEM ALTERAR NADA DO SEU CÓDIGO ORIGINAL **

        // Ataque médio - botão esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("MediumAttack");
        }

        // Ataque fraco - botão direito do mouse
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("WeakAttack");
        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }
    }
}