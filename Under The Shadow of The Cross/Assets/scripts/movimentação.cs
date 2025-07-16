using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float velocidademov = 5f;
    private Rigidbody2D rb;
    private Vector2 movimentação;
    private void Obixinhovaiandar()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
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

        rb.linearVelocity = movimentação * velocidademov;
    }

}

