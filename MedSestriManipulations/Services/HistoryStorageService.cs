using MedSestriManipulations.Models;
using System.Text.Json;

namespace MedSestriManipulations.Services
{
    public static class HistoryStorageService
    {
        private static readonly string FileName = "history.json";
        private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, FileName);

        public static async Task SaveAsync(List<RequestHistoryEntry> entries)
        {
            var json = JsonSerializer.Serialize(entries);
            await File.WriteAllTextAsync(FilePath, json);
        }

        public static async Task<List<RequestHistoryEntry>> LoadAsync()
        {
            if (!File.Exists(FilePath))
                return new List<RequestHistoryEntry>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<RequestHistoryEntry>>(json) ?? new List<RequestHistoryEntry>();
        }
    }
}
