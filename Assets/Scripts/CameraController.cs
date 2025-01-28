using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // este es el objetivo al que la camara seguira
    private Vector3 offset; // este es el desplazamiento entre la camara y el objetivo

    void Start()
    {  
       // Screen.SetResolution(480, 800, true); // establezer la res
        // calcular el desplazamiento inicial entre la camara y el objetivo
        offset = transform.position - target.position;
    }

    // se llama una vez por frame despues de que todas las funciones de update hayan sido llamadas
    void LateUpdate()
    {
        // calcular la nueva posicion de la camara en el eje z basado en la posicion del objetivo y el desplazamiento
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        // mover suavemente la camara hacia la nueva posicion
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }
}
