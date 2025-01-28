using UnityEngine;
using UnityEngine.SceneManagement; // PARA ABRIR EL MENU

public class MovementControlls : MonoBehaviour
{
    public float laneDistance = 2.5f; // Distancia entre carriles
    public float laneChangeSpeed = 2.0f; // Velocidad de cambio de carril
    public float forwardSpeed = 5.0f; // Velocidad de avance hacia adelante
    public float acceleration = 0.1f; // Aceleracion
    public bool isGameOver = false; // PARA COMPROBAR SI LA PARTIDA TERMINO


    private int lane = 0; // Carril actual del jugador
    private Vector3 targetPosition; // Posicion objetivo para el cambio de carril
    public SpawnManager spawnManager;
    public AudioSource swipeSound; // para el sonido del swipe
    public AudioSource deathSound; // para el sonido del swipe
    public GameManager gameManager;

    private AudioSource[] allAudioSources;  // Array para almacenar todos los AudioSources

    void Start()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        // Mover hacia adelante constantemente
        forwardSpeed += acceleration * Time.deltaTime;
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Saltar");
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Agacharse");
        }

        if (!isGameOver) // COMPRUEBA SI TERMINO LA PARTIDA PARA NO REPRODUCIR EL SONIDO
        {
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && lane < 1)
            {
                lane++;
                swipeSound.Play();
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && lane > -1)
            {
                lane--;
                swipeSound.Play();
            }
        }

        // Actualizar la posicion objetivo
        targetPosition = new Vector3(lane * laneDistance, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);

        // Interpola entre la posicion actual y la posicion objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) // los prefabs tienen que tener este Tag
        {
            isGameOver = true; // para q no funcione el swipe
            StopAllSounds(); // Detiene todos los sonidos
            deathSound.Play();
            gameManager.GameOver();
            //SceneManager.LoadScene("Menu"); // voy al menu si pierdo
            Time.timeScale = 0f;

           

        }
        else
        {
            spawnManager.SpawnTriggerEntered();
        }
    }

    public void RestartGame()
    {
        // Restablecer las variables y configuraciones
        Time.timeScale = 1f;  // Reanuda el tiempo
        RestartAllSounds(); // Reinicia todos los sonidos
        isGameOver = false; // para el swipe funcione                       
    }

    void StopAllSounds() //metodo para quitar todos los sonidos
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    void RestartAllSounds() // metodo para que los sonidos vuelvan a funcionar
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Play();
        }
    }

}
