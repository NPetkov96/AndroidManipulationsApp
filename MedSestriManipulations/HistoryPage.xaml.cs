using MedSestriManipulations.Models;
using MedSestriManipulations.Services;
using System.Windows.Input;

namespace MedSestriManipulations;

public partial class HistoryPage : ContentPage
{
    public ICommand ToggleCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand CopyCommand { get; }


    public HistoryPage()
    {
        InitializeComponent();
        ToggleCommand = new Command<RequestHistoryEntry>(OnToggle);
        RemoveCommand = new Command<RequestHistoryEntry>(OnRemove);
        HistoryList.ItemsSource = HistoryService.HistoryItems;
        BindingContext = this;
        CopyCommand = new Command<RequestHistoryEntry>(OnCopy);

    }

    private async void OnCopy(RequestHistoryEntry entry)
    {
        string website = "www.medsestri.com";
        string line = "-------------------------";
        var total = entry.SelectedProcedures.Sum(p => p.Price);
        var counter = 1;
        var message = $"Пациент: {entry.Name}\nЕГН: {entry.EGN}\nТелефон: {entry.Phone}\n\n" +
                              $"Избрани манипулации {entry.SelectedProcedures.Count}бр:\n" +
                              string.Join("\n", entry.SelectedProcedures.Select(p => $"{counter++}.{p.Name} - {p.Price:F2} лв")) +
                              $"\nУИН:{entry.UIN}" +
                              $"\n\nОбщо сума: {total:F2} лв\n{line} \nСума с отстъпка: {(total * 0.8m):F2} лв" +
                              $"\n{website}";

        await Clipboard.SetTextAsync(message);
    }


    private async void OnRemove(RequestHistoryEntry entry)
    {
        await HistoryService.RemoveAsync(entry);
    }

    private void OnToggle(RequestHistoryEntry entry)
    {
        entry.IsExpanded = !entry.IsExpanded;
    }
}