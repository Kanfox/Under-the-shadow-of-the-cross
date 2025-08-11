using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float velocidademov = 5f;
    private Rigidbody2D rb;
    private Vector2 movimentação;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        movimentação = new Vector2(moveDirection, 0f);
        rb.linearVelocity = movimentação * velocidademov;
    }
}
