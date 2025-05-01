using MedSestriManipulations.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MedSestriManipulations.Services
{
    public class ApiPatientService
    {
        private readonly HttpClient _httpClient;

        public ApiPatientService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://medsestribackendapi20250430210231-d9b9grdkdnecc0aw.italynorth-01.azurewebsites.net/")
            };
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/patients");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Patient>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
        }

        public async Task AddAsync(Patient patient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/patients", patient);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/patients/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
