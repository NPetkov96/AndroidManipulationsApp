using MedSestriManipulations.Services; // За достъп до LabResultsService и Parser
using System.Text.RegularExpressions;

public static class SmsParserService
{
    public static event Action<string, string>? OnCredentialsParsed;
    public static event Action<string, string, string, string>? OnPatientMatched; // name, date, id, pass

    public static async void OnSmsReceived(string body, string sender)
    {
        var (id, password) = ParseCredentials(body);

        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(password))
        {
            OnCredentialsParsed?.Invoke(id, password);

            var labService = new LabResultsService();
            var html = await labService.GetResultsHtmlAsync(id, password);

            if (html != null)
            {
                var parser = new LabResultsParser();
                var result = parser.ParsePatientInfo(html);

                if (result != null)
                {
                    var (name, birthDate) = result.Value;
                    await HistoryService.TryAutoAttachLabInfoAsync(name, birthDate, id, password);

                    // известяваме, че сме разпознали пациент
                    //OnPatientMatched?.Invoke(name, birthDate, id, password);
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
