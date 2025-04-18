using MedSestriManipulations.Models;
using MedSestriManipulations.Services;
using MvvmHelpers.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MedSestriManipulations;

public partial class HistoryPage : ContentPage, INotifyPropertyChanged
{
    public ICommand ToggleCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand CopyCommand { get; }

    public new event PropertyChangedEventHandler? PropertyChanged;

    private List<RequestHistoryEntry> _allItems = new();

    public HistoryPage()
    {

        InitializeComponent();
        BindingContext = this;

        _ = LoadHistoryAsync();

        ToggleCommand = new AsyncCommand<RequestHistoryEntry>(OnToggle);
        RemoveCommand = new AsyncCommand<RequestHistoryEntry>(OnRemove);
        CopyCommand = new AsyncCommand<RequestHistoryEntry>(OnCopy);

    }

    private async Task LoadHistoryAsync()
    {
        var items = await HistoryService.GetHistoryItemsAsync();
        HistoryList.ItemsSource = items;
        _allItems = items.ToList(); 
    }

    private void UpdateExpanderVisibility(string whichExpanded, bool isExpanded)
    {
        if (!isExpanded)
        {
            //Показваме всички и възстановяваме колоните
            ExpanderName.IsVisible = true;
            ExpanderEGN.IsVisible = true;
            ExpanderPhone.IsVisible = true;

            ExpanderName.SetValue(Grid.ColumnSpanProperty, 1);
            ExpanderName.SetValue(Grid.ColumnProperty, 0);

            ExpanderEGN.SetValue(Grid.ColumnSpanProperty, 1);
            ExpanderEGN.SetValue(Grid.ColumnProperty, 1);

            ExpanderPhone.SetValue(Grid.ColumnSpanProperty, 1);
            ExpanderPhone.SetValue(Grid.ColumnProperty, 2);

            return;
        }

        // Скриваме всички, освен избрания
        ExpanderName.IsVisible = whichExpanded == "name";
        ExpanderEGN.IsVisible = whichExpanded == "egn";
        ExpanderPhone.IsVisible = whichExpanded == "phone";

        switch (whichExpanded)
        {
            case "name":
                ExpanderName.SetValue(Grid.ColumnSpanProperty, 3);
                ExpanderName.SetValue(Grid.ColumnProperty, 0);
                break;

            case "egn":
                ExpanderEGN.SetValue(Grid.ColumnSpanProperty, 3);
                ExpanderEGN.SetValue(Grid.ColumnProperty, 0);
                break;

            case "phone":
                ExpanderPhone.SetValue(Grid.ColumnSpanProperty, 3);
                ExpanderPhone.SetValue(Grid.ColumnProperty, 0);
                break;
        }

    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnSearchByNameChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.Trim();
        HistoryList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
            ? _allItems
            : _allItems.Where(x => x.Name.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private void OnSearchByEGNChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.Trim();
        HistoryList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
            ? _allItems
            : _allItems.Where(x => x.EGN.StartsWith(keyword)).ToList();
    }

    private void OnSearchByPhoneChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.Trim();
        HistoryList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
            ? _allItems
            : _allItems.Where(x => x.Phone.StartsWith(keyword)).ToList();
    }

    private async Task OnCopy(RequestHistoryEntry entry)
    {
        await Clipboard.SetTextAsync(entry.Note);
    }

    private async Task OnRemove(RequestHistoryEntry entry)
    {
        await HistoryService.RemoveAsync(entry);
    }

    private async Task OnToggle(RequestHistoryEntry entry)
    {
        entry.IsExpanded = !entry.IsExpanded;
    }

    public bool IsNameExpanded
    {
        get => _isNameExpanded;
        set
        {
            if (_isNameExpanded != value)
            {
                _isNameExpanded = value;
                OnPropertyChanged();
                UpdateExpanderVisibility("name", value);
            }
        }
    }
    private bool _isNameExpanded;

    public bool IsEGNExpanded
    {
        get => _isEGNExpanded;
        set
        {
            if (_isEGNExpanded != value)
            {
                _isEGNExpanded = value;
                OnPropertyChanged();
                UpdateExpanderVisibility("egn", value);
            }
        }
    }
    private bool _isEGNExpanded;

    public bool IsPhoneExpanded
    {
        get => _isPhoneExpanded;
        set
        {
            if (_isPhoneExpanded != value)
            {
                _isPhoneExpanded = value;
                OnPropertyChanged();
                UpdateExpanderVisibility("phone", value);
            }
        }
    }
    private bool _isPhoneExpanded;
}