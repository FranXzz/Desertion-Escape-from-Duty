using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{
    // posiciones de los terrenos
    private int initAmount = 15; // cantidad inicial de prefabs
    private float plotSize = 30f; // tamaño de los terrenos
    private float xposLeft = -22; // posicion x para los terrenos de la izquierda
    private float xposRight = 22; // posicion x para los terrenos de la derecha
    private float lastZPos = -25f; // posicion z del ultimo terreno generado

    private float xposLeftLayer2 = -52; // posición x para la capa 2 izquierda
    private float xposRightLayer2 = 52; // posición x para la capa 2 derecha


    public List<GameObject> plots; // lista de prefabs de terrenos
    private List<GameObject> spawnedPlots = new List<GameObject>(); // lista para mantener un registro de los terrenos instanciados
    private int maxPlots = 80; // numero maximo de terrenos al mismo tiempo

    void Start()
    {
        // generar terrenos al inicio
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlot();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPlot()
    {
        // seleccionar un terreno aleatorio de la lista de prefabs para la izquierda y la derecha
        GameObject plotLeft = plots[Random.Range(0, plots.Count)];
        GameObject plotRight = plots[Random.Range(0, plots.Count)];

        //terrenos 2 capa
        GameObject plotLeftLayer2 = plots[Random.Range(0, plots.Count)];
        GameObject plotRightLayer2 = plots[Random.Range(0, plots.Count)];




        // calcular la posicion z para el nuevo terreno
        float zPos = lastZPos + plotSize;

        // instanciar los terrenos en las posiciones calculadas
        GameObject leftInstance = Instantiate(plotLeft, new Vector3(xposLeft, 0.025f, zPos), plotLeft.transform.rotation);
        GameObject rightInstance = Instantiate(plotRight, new Vector3(xposRight, 0.025f, zPos), new Quaternion(0, 180, 0, 0));

        //terrenos 2 capa
        GameObject leftInstanceLayer2 = Instantiate(plotLeftLayer2, new Vector3(xposLeftLayer2, 0.025f, zPos), plotLeft.transform.rotation);
        GameObject rightInstanceLayer2 = Instantiate(plotRightLayer2, new Vector3(xposRightLayer2, 0.025f, zPos), new Quaternion(0, 180, 0, 0));



        // agregar los terrenos instanciados a la lista DE TERRENOS TOTALES QUE SERVIRA PARA ELIMINAR LOS DE MAS ATRAS
        spawnedPlots.Add(leftInstance);
        spawnedPlots.Add(rightInstance);
        spawnedPlots.Add(leftInstanceLayer2);
        spawnedPlots.Add(rightInstanceLayer2);



        // si la lista de terrenos instanciados excede el maximo permitido destruye los mas antiguos
        while (spawnedPlots.Count > maxPlots)
        {
            GameObject oldestPlot = spawnedPlots[0];
            spawnedPlots.RemoveAt(0);
            Destroy(oldestPlot);
        }

        // actualizar la posicion z para el siguiente terreno
        lastZPos += plotSize;
    }
}
