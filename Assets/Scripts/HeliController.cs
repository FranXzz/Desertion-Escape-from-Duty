using UnityEngine;

public class HeliController : MonoBehaviour
{
    public Transform player;  // El transform del jugador que el helicoptero seguira
    public float followSpeed = 4f;  // Velocidad de seguimiento del helicoptero
    public float heightAboveGround = 10f;  // Altura del helicoptero sobre el suelo
    public float distanceAheadPlayer = 17f;  // distancia adelante del jugador

    private void Update()
    {
        if (player != null)
        {
            // Obtener la posicion del suelo
            float groundHeight = 0f;
            GameObject ground = GameObject.Find("Ground");
            if (ground != null)
            {
                groundHeight = ground.transform.position.y;
            }

            // Calcula la nueva posicion del helicoptero adelante y sobre el suelo
            Vector3 targetPosition = player.position + player.forward * distanceAheadPlayer + Vector3.up * (groundHeight + heightAboveGround);

            // Mueve suavemente el helicoptero hacia la nueva posicion
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
