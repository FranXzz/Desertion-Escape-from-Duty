using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI uiDistance;
    public TextMeshProUGUI uiPuntaje;
    public GameObject GameOverMenu;

    // Referencia al script de PauseMenu
    [SerializeField] public PauseMenu pauseMenuScript;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z); // para calcular la distancia para el coso de metros

        uiDistance.text = distance.ToString() + "m";
        uiPuntaje.text = (distance * 3.14).ToString("0.00") + " points"; // Multiplica la distancia por 100 y lo convierte a string

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si el juego no no esta pausado, pausar el juego, sino, reanudarlo
            if (Time.timeScale == 1f) // Si el juego esta en marcha
            {
                pauseMenuScript.Pause(); // Llama al metodo de pausa desde el script de PauseMenu
            }
            else // Si el juego está pausado
            {
                pauseMenuScript.ResumeGame(); // Llama al metodo de reanudar el juego
            }
        }

    }

    public void GameOver() //metodo para el game over
    {
        int currentScore = Mathf.RoundToInt(player.transform.position.z);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        GameOverMenu.SetActive(true);

    }


}
