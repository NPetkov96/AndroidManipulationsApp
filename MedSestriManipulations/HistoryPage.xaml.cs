using MedSestriManipulations.Models;
using MedSestriManipulations.Services;
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


    private List<RequestHistoryEntry> _allItems;

    public HistoryPage()
    {

        InitializeComponent();
        BindingContext = this;

        LoadHistoryAsync();
        //LoadHistory();


        ToggleCommand = new Command<RequestHistoryEntry>(OnToggle);
        RemoveCommand = new Command<RequestHistoryEntry>(OnRemove);
        CopyCommand = new Command<RequestHistoryEntry>(OnCopy);

    }

    private async Task LoadHistoryAsync()
    {
        HistoryList.ItemsSource = await HistoryService.GetHistoryItemsAsync();
    }


    //private void UpdateExpanderVisibility(string whichExpanded, bool isExpanded)
    //{
    //    if (!isExpanded)
    //    {
    //        // Показваме всички и възстановяваме колоните
    //        //ExpanderName.IsVisible = true;
    //        //ExpanderEGN.IsVisible = true;
    //        //ExpanderPhone.IsVisible = true;

    //        //ExpanderName.SetValue(Grid.ColumnSpanProperty, 1);
    //        //ExpanderName.SetValue(Grid.ColumnProperty, 0);

    //        //ExpanderEGN.SetValue(Grid.ColumnSpanProperty, 1);
    //        //ExpanderEGN.SetValue(Grid.ColumnProperty, 1);

    //        //ExpanderPhone.SetValue(Grid.ColumnSpanProperty, 1);
    //        //ExpanderPhone.SetValue(Grid.ColumnProperty, 2);

    //        return;
    //    }

    //    // Скриваме всички, освен избрания
    //    ExpanderName.IsVisible = whichExpanded == "name";
    //    //ExpanderEGN.IsVisible = whichExpanded == "egn";
    //    //ExpanderPhone.IsVisible = whichExpanded == "phone";

    //    if (whichExpanded == "name")
    //    {
    //        //ExpanderName.SetValue(Grid.ColumnSpanProperty, 3);
    //        //ExpanderName.SetValue(Grid.ColumnProperty, 0);
    //    }
    //    else if (whichExpanded == "egn")
    //    {
    //        //ExpanderEGN.SetValue(Grid.ColumnSpanProperty, 3);
    //        //ExpanderEGN.SetValue(Grid.ColumnProperty, 0);
    //    }
    //    else if (whichExpanded == "phone")
    //    {
    //        //ExpanderPhone.SetValue(Grid.ColumnSpanProperty, 3);
    //        //ExpanderPhone.SetValue(Grid.ColumnProperty, 0);
    //    }
    //}

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

    //private void OnSearchByEGNChanged(object sender, TextChangedEventArgs e)
    //{
    //    var keyword = e.NewTextValue?.Trim();
    //    HistoryList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
    //        ? _allItems
    //        : _allItems.Where(x => x.EGN.StartsWith(keyword)).ToList();
    //}

    //private void OnSearchByPhoneChanged(object sender, TextChangedEventArgs e)
    //{
    //    var keyword = e.NewTextValue?.Trim();
    //    HistoryList.ItemsSource = string.IsNullOrWhiteSpace(keyword)
    //        ? _allItems
    //        : _allItems.Where(x => x.Phone.StartsWith(keyword)).ToList();
    //}


    private async void OnCopy(RequestHistoryEntry entry)
    {
        //if (entry.UIN == string.Empty)
        //{
        //    entry.UIN = "няма";
        //}

        //string website = "www.medsestri.com";
        //string line = "-------------------------";
        //var total = entry.SelectedProcedures.Sum(p => p.Price);
        //var counter = 1;
        //var message = $"Пациент: {entry.Name}\nЕГН: {entry.EGN}\nТелефон: {entry.Phone}\n\n" +
        //                      $"Избрани манипулации {entry.SelectedProcedures.Count}бр:\n" +
        //                      string.Join("\n", entry.SelectedProcedures.Select(p => $"{counter++}.{p.Name} - {p.Price:F2} лв")) +
        //                      $"\nУИН:{entry.UIN}" +
        //                      $"\n\nОбщо сума: {total:F2} лв\n{line} \nСума с отстъпка: {(total * 0.8m):F2} лв" +
        //                      $"\n{website}";

        await Clipboard.SetTextAsync(entry.Note);
    }


    private async void OnRemove(RequestHistoryEntry entry)
    {
        await HistoryService.RemoveAsync(entry);
    }

    private void OnToggle(RequestHistoryEntry entry)
    {
        entry.IsExpanded = !entry.IsExpanded;
    }

//    public bool IsNameExpanded
//    {
//        get => _isNameExpanded;
//        set
//        {
//            if (_isNameExpanded != value)
//            {
//                _isNameExpanded = value;
//                OnPropertyChanged();
//                UpdateExpanderVisibility("name", value);
//            }
//        }
//    }
//    private bool _isNameExpanded;

//    public bool IsEGNExpanded
//    {
//        get => _isEGNExpanded;
//        set
//        {
//            if (_isEGNExpanded != value)
//            {
//                _isEGNExpanded = value;
//                OnPropertyChanged();
//                //UpdateExpanderVisibility("egn", value);
//            }
//        }
//    }
//    private bool _isEGNExpanded;

//    public bool IsPhoneExpanded
//    {
//        get => _isPhoneExpanded;
//        set
//        {
//            if (_isPhoneExpanded != value)
//            {
//                _isPhoneExpanded = value;
//                OnPropertyChanged();
//                UpdateExpanderVisibility("phone", value);
//            }
//        }
//    }
//    private bool _isPhoneExpanded;
}