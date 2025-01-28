using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    public MovementControlls movementController;  // INSTANCIA DEL MOVEMENT PLAYER

    public void Jugar()
    {
        SceneManager.LoadScene("Game");
        // Encuentra el objeto con el script MovementControlls
        MovementControlls movementController = FindObjectOfType<MovementControlls>();

        // Si se encuentra el objeto, llama a su metodo RestartGame
        if (movementController != null)
        {
            movementController.RestartGame();
        }


    }

    public void Salir()
    {
        Application.Quit();
    }

    public void GoMenu()
    {
        // Encuentra el objeto con el script MovementControlls
        MovementControlls movementController = FindObjectOfType<MovementControlls>();

        // Si se encuentra el objeto, llama a su metodo RestartGame
        if (movementController != null)
        {
            movementController.RestartGame();
        }

        // Carga la escena del menú
        SceneManager.LoadScene("Menu");
    }




}
