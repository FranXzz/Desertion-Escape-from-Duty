using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Transform player; // referencia al jugador

    private int spawnInterval = 11; // intervalo de aparicion de obstaculos
    private int lastSpawnZ = 30; // posicion z del ultimo obstaculo generado
    private int SpawnAmount = 4; // cantidad de obstaculos a generar
    private int maxObstacles = 30; // numero maximo de obstaculos al mismo tiempo

    public List<GameObject> obstacles; // lista de prefabs de obstaculos
    private List<GameObject> spawnedObstacles = new List<GameObject>(); // lista para mantener un registro de los obstaculos instanciados

    void Start()
    {
        SpawnObstacles(); // generar obstaculos al inicio
    }

    public void SpawnObstacles()
    {
        for (int i = 0; i < SpawnAmount; i++)
        {
            lastSpawnZ += spawnInterval;
            if (Random.Range(0, 2) == 0)
            {
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
                GameObject instance = Instantiate(obstacle, new Vector3(0, 0.25f, lastSpawnZ), obstacle.transform.rotation);

                spawnedObstacles.Add(instance);
                // Si hay mas obstaculos que los permitidos empezar a borrar los mas antiguos
                while (spawnedObstacles.Count > maxObstacles)
                {
                    GameObject oldestObstacle = spawnedObstacles[0];  
                    if (oldestObstacle.transform.position.z < player.position.z)   // Solo eliminar el obstaculo si el jugador ya ha pasado por él
                    {
                        spawnedObstacles.RemoveAt(0); // Eliminar de la lista
                        Destroy(oldestObstacle, 2f); // Agrega un retraso de 2 segundos antes de destruir el objeto
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}