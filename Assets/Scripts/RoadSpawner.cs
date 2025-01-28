using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads; // lista de calles
    private float offset = 30f; // distancia entre las calles que aparecen

    void Start()
    {
        // si la lista de calles no esta vacia
        if (roads != null && roads.Count > 0)
        {
            // ordenar las calles por su posicion en el eje z
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        // obtener la primera calle de la lista
        GameObject moveRoad = roads[0];
        // remover la calle de la lista
        roads.Remove(moveRoad);
        // calcular la nueva posicion z para la calle
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        // mover la calle a la nueva posicion
        moveRoad.transform.position = new Vector3(0, 0, newZ);
        // agregar la calle al final de la lista
        roads.Add(moveRoad);
    }
}
