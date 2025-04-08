using MedSestriManipulations.Models;
using MedSestriManipulations.Services;
using System.Windows.Input;

namespace MedSestriManipulations;

public partial class HistoryPage : ContentPage
{
    public ICommand ToggleCommand { get; }
    public ICommand RemoveCommand { get; }

    public HistoryPage()
    {
        InitializeComponent();
        ToggleCommand = new Command<RequestHistoryEntry>(OnToggle);
        RemoveCommand = new Command<RequestHistoryEntry>(OnRemove);
        HistoryList.ItemsSource = HistoryService.HistoryItems;
        BindingContext = this;
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