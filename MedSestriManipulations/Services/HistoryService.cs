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
            HistoryItems = new ObservableCollection<RequestHistoryEntry>(loaded);
        }

        public static async Task AddAsync(RequestHistoryEntry entry)
        {
            HistoryItems.Insert(0, entry);
            await HistoryStorageService.SaveAsync(HistoryItems.ToList());
        }

        public static async Task RemoveAsync(RequestHistoryEntry entry)
        {
            HistoryItems.Remove(entry);
            await HistoryStorageService.SaveAsync(HistoryItems.ToList());
        }
    }

}
