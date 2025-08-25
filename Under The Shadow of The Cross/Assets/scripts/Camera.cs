using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Personagem a ser seguido
    public float offsetX = 0f;         // Deslocamento horizontal
    public float offsetZ = -10f;       // Profundidade fixa (padr�o 2D)
    public float smoothSpeed = 0.125f; // Suaviza��o da c�mera

    public float minX = 0f;            // Limite m�nimo no eixo X
    public float maxX = 50f;           // Limite m�ximo no eixo X

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        // Posi��o alvo apenas no eixo X
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(player.position.x + offsetX, minX, maxX), // restringe movimento
            transform.position.y, // mant�m Y fixo
            offsetZ
        );

        // Movimento suave da c�mera
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );
    }
}