using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class PlayerNameManager : MonoBehaviour
{
    public GameObject namePromptPanel; // Panel donde el jugador escribe su nombre
    public TMPro.TMP_InputField nameInputField; // Input donde el jugador escribe su nombre

    private async void Start()
    {
        await InitializeUGS();
        await SignInAnonymously();
        CheckForPlayerName();
    }

    // Inicializar UGS
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

    //  Iniciar sesión anónima
    private async Task SignInAnonymously()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
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
    }

    //  Revisar si el jugador ya tiene un nombre
    private async void CheckForPlayerName()
    {
        string playerId = AuthenticationService.Instance.PlayerId;

        //  Si el nombre no está en PlayerPrefs, intenta obtenerlo desde Unity Services
        if (!PlayerPrefs.HasKey($"PlayerName_{playerId}"))
        {
            string fetchedName = AuthenticationService.Instance.PlayerName; // Usa PlayerName directamente

            if (string.IsNullOrEmpty(fetchedName) || fetchedName.StartsWith("Player#"))
            {
                Debug.Log("No se encontró un nombre válido. Mostrando panel...");
                namePromptPanel.SetActive(true); // Mostrar el panel de nombre
            }
            else
            {
                PlayerPrefs.SetString($"PlayerName_{playerId}", fetchedName);
                PlayerPrefs.Save();
                Debug.Log($"Nombre encontrado en Unity y guardado: {fetchedName}");
            }
        }
    }


    //  Guardar el nombre cuando el jugador lo escriba
    public async void SavePlayerName()
    {
        string playerName = nameInputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            string playerId = AuthenticationService.Instance.PlayerId;
            PlayerPrefs.SetString($"PlayerName_{playerId}", playerName);
            PlayerPrefs.Save();

            try
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
                Debug.Log($"Nombre guardado en UGS: {playerName}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error al actualizar el nombre en UGS: {ex.Message}");
            }

            namePromptPanel.SetActive(false); // Ocultar el panel después de guardar el nombre
        }
    }
}
