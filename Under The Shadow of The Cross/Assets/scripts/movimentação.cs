using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float movimentoHorizontal;
    private Rigidbody2D rb;

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
        movimentoHorizontal = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.velocity =
    }
}
