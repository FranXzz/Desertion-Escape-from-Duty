using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private string leaderboardID = "Leaderboard"; // ID de la tabla de clasificación

    async void Start()
    {
        await InitializeUGS();
    }

    async Task InitializeUGS()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async Task SendScore(int score)
    {
        string playerName = AuthenticationService.Instance.PlayerName;

        // Si el nombre no está configurado, lo configuramos ahora.
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Jugador_" + AuthenticationService.Instance.PlayerId; // Nombre predeterminado
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        }

          await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);
         Debug.Log("Puntaje enviado: " + score);
    }
}
