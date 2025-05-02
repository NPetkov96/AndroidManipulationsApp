using MedSestriManipulations.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace MedSestriManipulations.Services.History
{
    public class HistoryService
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Patient> HistoryItems { get; private set; } = new();

        public HistoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<HttpResponseMessage> AddAsync(Patient entry)
        {
            return await _httpClient.PostAsJsonAsync("api/patients", entry);
        }

        public async Task RemoveAsync(Patient entry)
        {
            var response = await _httpClient.DeleteAsync($"api/patients/{entry.Id}");
            if (response.IsSuccessStatusCode)
            {
                HistoryItems.Remove(entry);
            }
        }

        public async Task ReadAllPatientsFromCloud()
        {
            var response = await _httpClient.GetAsync("api/patients");

            var json = await response.Content.ReadAsStringAsync();
            var patients = JsonSerializer.Deserialize<List<Patient>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            HistoryItems = new ObservableCollection<Patient>(patients.OrderByDescending(p => p.CreatedAt));
        }

        public async Task<IEnumerable<Patient>> GetHistoryItemsAsync()
        {
            await ReadAllPatientsFromCloud();
            return await Task.Run(() => HistoryItems);
        }
        public async Task TryAutoAttachLabInfoAsync(string name, string birthDate, string labId, string labPassword)
        {
            string Normalize(string text) => text?.ToLower().Replace(" ", "").Trim() ?? "";

            string? GetBirthDateFromEGN(string egn)
            {
                if (string.IsNullOrWhiteSpace(egn) || egn.Length < 10)
                    return null;

                try
                {
                    int year = int.Parse(egn.Substring(0, 2));
                    int month = int.Parse(egn.Substring(2, 2));
                    int day = int.Parse(egn.Substring(4, 2));

                    if (month > 40) { year += 2000; month -= 40; }
                    else if (month > 20) { year += 1800; month -= 20; }
                    else { year += 1900; }

                    return new DateTime(year, month, day).ToString("dd.MM.yyyy");
                }
                catch
                {
                    return null;
                }
            }

            var matched = HistoryItems
                .Where(p => p.LabPassword == "" && p.LabPassword == "")
                .FirstOrDefault(p => Normalize(p.FullName) == Normalize(name) &&
                GetBirthDateFromEGN(p.EGN) == birthDate);

            if (matched != null)
            {
                if (!matched.Note.Contains("Lab ID"))
                {
                    matched.LabId = labId;
                    matched.LabPassword = labPassword;

                    string labInfo = $"ID: {matched.LabId}\nPassword: {matched.LabPassword}";

                    matched.Note += (string.IsNullOrWhiteSpace(matched.Note) ? "" : "\n \n") + labInfo;
                    await _httpClient.PutAsJsonAsync($"api/patients/{matched.Id}", matched);
                }
            }
        }

        public async Task<List<string>> GetAllPreviousNamesAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.FullName.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public async Task<List<string>> GetAllPreviousEGNAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.EGN.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public async Task<List<string>> GetAllPreviousPhonesAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.PhoneNumber.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }
    }
}
