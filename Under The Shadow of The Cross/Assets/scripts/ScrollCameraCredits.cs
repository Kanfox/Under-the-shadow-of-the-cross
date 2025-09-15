using UnityEngine;

public class ScrollCameraCredits : MonoBehaviour
{
    public float speed = 0.5f;  // velocidade de descida
    public float endY = -10f;   // posi��o final da c�mera no Y

    void Update()
    {
        // Move apenas a c�mera para baixo
        if (transform.position.y > endY)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }
}
