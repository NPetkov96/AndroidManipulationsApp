using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MedSestriManipulations.Services
{
    public class LabResultsService
    {
        private readonly HttpClient _httpClient;

        public LabResultsService()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://results.bodimed.com/new/bodimed/index.html")
            };
        }

        public async Task<string?> GetResultsHtmlAsync(string id, string password)
        {
            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://results.bodimed.com")
            };

            // 1. Първо GET заявка, за да вземем PHPSESSID
            await client.GetAsync("/new/bodimed/index.html");

            // 2. Подготвяме POST съдържание с правилни имена
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("idnap", id),
                new KeyValuePair<string, string>("pass", password)
            });

            // 3. Header-и като от браузър
            client.DefaultRequestHeaders.Referrer = new Uri("https://results.bodimed.com/new/bodimed/index.html");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            var response = await client.PostAsync("/new/results_patient.php", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var html = Encoding.GetEncoding("windows-1251").GetString(bytes);

            return html;
        }
    }
}
