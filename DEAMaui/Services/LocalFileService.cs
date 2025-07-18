using DEAModels;
using System.Diagnostics;

namespace DEAMaui.Services
{
    public class LocalFileService : ILocalFileService
    {
        private readonly string _filePath;

        public LocalFileService()
        {
            string appDataDir = FileSystem.Current.AppDataDirectory;
            string fileName = "matches_log.txt";
            _filePath = Path.Combine(appDataDir, fileName);
            Debug.WriteLine($"Ruta del log de partidos: {_filePath}");
        }

        public async Task LogMatchActionAsync(string action, Match match)
        {
            try
            {

                string logEntry =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - ACCIÓN: {action.ToUpper()}\n" +
                    $"  - Partido ID: {match.IdMatch}\n" +
                    $"  - Fecha: {match.MatchDate:dd/MM/yyyy}\n" +
                    $"  - Marcador: {match.HomeTeam?.Name ?? "Equipo Local"} {match.HomeGoals} - {match.AwayGoals} {match.AwayTeam?.Name ?? "Equipo Visitante"}\n" +
                    $"  - Competición: {match.Competition?.Name ?? "N/A"}\n" +
                    $"--------------------------------------------------\n\n";


                await File.AppendAllTextAsync(_filePath, logEntry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al escribir en el log local: {ex.Message}");
            }
        }
    }
}