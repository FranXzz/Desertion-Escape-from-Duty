using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private MovementControlls movementControlls; 


    void Start() 
    {
        movementControlls = GameObject.FindObjectOfType<MovementControlls>();
    }

    public void Pause()
    {
        if (movementControlls.isGameOver==false) //compruebo si el jugador esta muerto para que no pueda pausar
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.pause = true; //pausa todo el audio
        }



    }

    public void ResumeGame()
    {
        if (movementControlls.isGameOver == false) //compruebo si el jugador esta muerto para que no pueda pausar
        {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; //reanuda todo el audio

        }
    }

    public void Home()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; //reanuda todo el audio
        SceneManager.LoadScene("Menu");
    }






}