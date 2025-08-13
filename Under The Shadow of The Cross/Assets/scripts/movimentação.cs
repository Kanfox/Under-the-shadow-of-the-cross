using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forcaDoPulo = 10f;
    public float velocidadeMovimento = 5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Verifica se está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaDoPulo);
        }
    }

    private void FixedUpdate()
    {
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
        }

        rb.linearVelocity = new Vector2(moveDirection * velocidadeMovimento, rb.linearVelocity.y);
    }
}
