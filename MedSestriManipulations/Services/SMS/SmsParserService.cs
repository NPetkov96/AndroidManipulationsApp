// Refactored SmsParserService - no static, DI-ready

using MedSestriManipulations.Services.History;
using MedSestriManipulations.Services.Laboratory;
using System.Text.RegularExpressions;

namespace MedSestriManipulations.Services.SMS
{
    public class SmsParserService
    {
        private readonly HistoryService _historyService;
        private readonly LabResultsService _labResultsService;
        private readonly LabResultsParser _labResultsParser;

        // Events as instance members
        public event Action<string, string>? CredentialsParsed;
        public event Action<string, string, string, string>? PatientMatched;

        public SmsParserService(HistoryService historyService)
        {
            _historyService = historyService;
            _labResultsService = new LabResultsService();
            _labResultsParser = new LabResultsParser();
        }

        public async Task HandleSmsAsync(string body, string sender)
        {
            var (id, password) = ParseCredentials(body);

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password))
            {
                CredentialsParsed?.Invoke(id, password);

                var html = await _labResultsService.GetResultsHtmlAsync(id, password);
                if (html != null)
                {
                    var result = _labResultsParser.ParsePatientInfo(html);
                    if (result != null)
                    {
                        var (name, birthDate) = result.Value;
                        await _historyService.TryAutoAttachLabInfoAsync(name, birthDate, id, password);

                        PatientMatched?.Invoke(name, birthDate, id, password);
                        LastSmsReadTracker.SaveLastReadTime(DateTime.Now);
                    }
                }
            }
        }

        private static (string id, string password) ParseCredentials(string sms)
        {
            var match = Regex.Match(sms, @"ID:\s*(\d+)\s*,\s*Parola:\s*(\d+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var id = match.Groups[1].Value;
                var password = match.Groups[2].Value;
                return (id, password);
            }

            return (null, null);
        }
    }
}
