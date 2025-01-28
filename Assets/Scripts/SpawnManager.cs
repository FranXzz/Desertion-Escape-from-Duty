using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    RoadSpawner roadSpawner; //rereferencia al script de calles
    PlotSpawner plotSpawner; //referencia al plot spawner
    Obstacles obstacles; //referencia al script de obstáculos

    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();//metodo roadspwaner
        plotSpawner = GetComponent<PlotSpawner>();//metodo plotspawner
        obstacles = GetComponent<Obstacles>(); //metodo obstacles
    }

  
    void Update()
    {
        
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
        plotSpawner.SpawnPlot(); //llamando al metodo
        obstacles.SpawnObstacles(); //llamando al metodo de obstáculos
    }

}
