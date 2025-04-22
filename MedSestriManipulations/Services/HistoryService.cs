using MedSestriManipulations.Models;
using System.Collections.ObjectModel;

namespace MedSestriManipulations.Services
{
    public static class HistoryService
    {
        public static ObservableCollection<RequestHistoryEntry> HistoryItems { get; private set; } = new();

        public static async Task InitializeAsync()
        {
            var loaded = await HistoryStorageService.LoadAsync();
            loaded.OrderByDescending(e => e.Date).ToList();

            //Филтрирай записите до последните 10 дни
            var recent = loaded
                .Where(e => e.Date >= DateTime.Now.AddDays(-10))
                .OrderByDescending(e => e.Date)
                .ToList();

            HistoryItems = new ObservableCollection<RequestHistoryEntry>(recent);

            // Презапиши файла без старите
            await HistoryStorageService.SaveAsync(loaded);
        }


        public static async Task TryAutoAttachLabInfoAsync(string name, string birthDate, string labId, string labPassword)
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

                    return new DateTime(year, month, day).ToString("MM.dd.yyyy");
                }
                catch
                {
                    return null;
                }
            }

            var matched = HistoryItems.FirstOrDefault(p =>
                Normalize(p.Name) == Normalize(name) &&
                GetBirthDateFromEGN(p.EGN) == birthDate);

            if (matched != null)
            {
                if (!matched.Note.Contains("Lab ID"))
                {
                    matched.LabId = labId;
                    matched.LabPassword = labPassword;

                    string labInfo = $"ID: {matched.LabId}\nPassword: {matched.LabPassword}";

                    matched.Note += (string.IsNullOrWhiteSpace(matched.Note) ? "" : "\n \n") + labInfo;
                    await HistoryStorageService.SaveAsync(HistoryItems.ToList());
                }
            }
        }




        public static async Task<IEnumerable<RequestHistoryEntry>> GetHistoryItemsAsync()
        {
            return await Task.Run(() => HistoryItems);
        }

        public static async Task AddAsync(RequestHistoryEntry entry)
        {
            HistoryItems.Insert(0, entry);

            // Премахни стари (над 10 дни)
            var recent = HistoryItems
                .Where(e => e.Date >= DateTime.Now.AddDays(-10))
                .OrderByDescending(e => e.Date)
                .ToList();

            //// Обнови колекцията
            HistoryItems.Clear();
            foreach (var e in recent)
                HistoryItems.Add(e);

            // Запази във файла
            await HistoryStorageService.SaveAsync(recent);
        }

        public static async Task RemoveAsync(RequestHistoryEntry entry)
        {
            HistoryItems.Remove(entry);
            await HistoryStorageService.SaveAsync(HistoryItems.ToList());
        }

        public static async Task<List<string>> GetAllPreviousNamesAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.Name.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public static async Task<List<string>> GetAllPreviousEGNAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.EGN.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public static async Task<List<string>> GetAllPreviousPhonesAsync()
        {
            var history = await GetHistoryItemsAsync();
            return history
                .Select(h => h.Phone.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }
    }

}
