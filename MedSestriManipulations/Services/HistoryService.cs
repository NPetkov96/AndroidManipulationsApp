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
