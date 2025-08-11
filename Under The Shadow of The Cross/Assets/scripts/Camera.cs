using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // arraste o personagem aqui no inspector
    public float smoothSpeed = 0.125f; // suavidade do movimento
    public Vector3 offset; // ajuste para posi��o inicial da c�mera

    void LateUpdate()
    {
        // Pega a posi��o atual da c�mera
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);

        // Suaviza o movimento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplica a posi��o
        transform.position = smoothedPosition;
    }
}