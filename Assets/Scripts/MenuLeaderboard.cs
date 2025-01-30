using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class MenuLeaderboard : MonoBehaviour
{
    public GameObject namePromptPanel; // Panel que pide el nombre
    public TMP_InputField nameInputField; // Input Field del nombre
    public TextMeshProUGUI leaderboardText; // Texto de la tabla de clasificación
    private string leaderboardID = "Leaderboard"; // Asegúrate de que este ID sea correcto en Unity

    // Referencia al LeaderboardManager
    private LeaderboardManager leaderboardManager;

    async void Start()
    {
        leaderboardManager = FindObjectOfType<LeaderboardManager>(); // Obtener la referencia al LeaderboardManager

        await InitializeUGS();
        await SignInAnonymously();

        string playerId = AuthenticationService.Instance.PlayerId;

        // Verificar si el nombre está guardado en PlayerPrefs con la ID del jugador
        if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
        {
            namePromptPanel.SetActive(true); // Mostrar el panel para ingresar el nombre
        }
        else
        {
            namePromptPanel.SetActive(false); // Ocultar el panel si ya está guardado
            await FetchLeaderboard(); // Cargar la tabla de clasificación
        }
    }

    private async Task InitializeUGS()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("UGS Initialized.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error initializing UGS: {ex.Message}");
        }
    }

    private async Task SignInAnonymously()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("User already signed in.");
            return;
        }

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"User signed in! Player ID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"Authentication Error: {ex.Message}");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Request Failed: {ex.Message}");
        }
    }

    public async void SavePlayerName()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName); // Actualizar el nombre en Unity Services
                namePromptPanel.SetActive(false); // Ocultar el panel después de guardar el nombre
                Debug.Log($"Nombre guardado: {playerName}");

                // Llamar a la Leaderboard para actualizar con el nuevo nombre
                await FetchLeaderboard();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error al actualizar el nombre: {ex.Message}");
            }
        }
    }

    async Task FetchLeaderboard()
    {
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

            leaderboardText.text = " Leaderboard \n";
            int rank = 1;
            foreach (var entry in scores.Results)
            {
                string playerId = entry.PlayerId;
                string playerName = entry.PlayerName; // Obtener el nombre del jugador directamente
                leaderboardText.text += $"{rank}. {playerName} - {entry.Score} points\n";
                rank++;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error fetching leaderboard: {ex.Message}");
        }
    }

    // Aquí cambias la llamada a SendScore para usar la referencia de LeaderboardManager
    public async void SubmitScore(int score)
    {
        if (leaderboardManager != null)
        {
            // Ahora podemos usar await porque SendScore devuelve un Task
            await leaderboardManager.SendScore(score);
            await FetchLeaderboard(); // Refrescar la tabla de clasificación después de enviar el puntaje
        }
        else
        {
            Debug.LogError("LeaderboardManager no encontrado.");
        }
    }
}
