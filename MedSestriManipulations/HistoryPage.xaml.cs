using MedSestriManipulations.Models;
using MedSestriManipulations.Services.History;
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
    public ICommand SendSmsCommand { get; }

    private readonly HistoryService _historyService;

    public new event PropertyChangedEventHandler? PropertyChanged;

    private List<Patient> _allItems = new();

    public HistoryPage(HistoryService historyService)
    {

        InitializeComponent();

        BindingContext = this;
        _historyService = historyService;

        _ = LoadHistoryAsync();

        ToggleCommand = new AsyncCommand<Patient>(OnToggle);
        RemoveCommand = new AsyncCommand<Patient>(OnRemove);
        CopyCommand = new AsyncCommand<Patient>(OnCopy);
        SendSmsCommand = new AsyncCommand<Patient>(SendSmsToPatient);

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadHistoryAsync();
    }

    private async Task SendSmsToPatient(Patient entry)
    {
        if (string.IsNullOrWhiteSpace(entry.LabId) || string.IsNullOrWhiteSpace(entry.LabPassword))
        {
            await Application.Current.MainPage.DisplayAlert("Грешка", "Няма открито ID и парола в бележката.", "OK");
            return;
        }
        
        string message = $"Здравейте, резултатите ви са налични.\nID: {entry.LabId}\nПарола: {entry.LabPassword}";

        try
        {
            var sms = new SmsMessage(message, entry.PhoneNumber);
            await Sms.Default.ComposeAsync(sms);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Грешка при изпращане", ex.Message, "OK");
        }
    }

    private async Task LoadHistoryAsync()
    {
        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            var items = await _historyService.GetHistoryItemsAsync();
            HistoryList.ItemsSource = items;
            _allItems = items.ToList();
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
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
            : _allItems.Where(x => x.FullName.StartsWith(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
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
            : _allItems.Where(x => x.PhoneNumber.StartsWith(keyword)).ToList();
    }

    private async Task OnCopy(Patient entry)
    {
        await Clipboard.SetTextAsync(entry.Note);
    }

    private async Task OnRemove(Patient entry)
    {
        await _historyService.RemoveAsync(entry);
    }

    private async Task OnToggle(Patient entry)
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