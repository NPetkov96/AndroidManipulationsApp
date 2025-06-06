using MedSestriManipulations.Models;
using MedSestriManipulations.Services;
using MedSestriManipulations.Services.History;
using MedSestriManipulations.Services.SMS;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;


namespace MedSestriManipulations
{
    public partial class MainPage : ContentPage
    {
        private List<string> AllNames = new();
        private List<string> AllEgn = new();
        private List<string> AllPhones = new();
        public ObservableCollection<string> NameSuggestions { get; set; } = new();
        public ObservableCollection<string> EGNSuggestions { get; set; } = new();
        public ObservableCollection<string> PhoneSuggestions { get; set; } = new();

        public ObservableCollection<MedicalProcedureViewModel> CheckedProcedures = new();
        public ObservableCollection<MedicalProcedureViewModel> Procedures = new();
        private readonly HistoryService _historyService;
        private readonly SmsPermissionService _smsPermissionService;
        private readonly PaginationState _paginationState;

        public MainPage(HistoryService historyService, SmsPermissionService smsPermissionService, PaginationState paginationState)
        {
            InitializeComponent();
            BindingContext = this;
            _historyService = historyService;
            _smsPermissionService = smsPermissionService;
            _paginationState = paginationState;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (CheckedProcedures.Count == 0) await LoadAsyncIfNeeded();

            if (AllNames.Count == 0 || AllEgn.Count == 0 || AllPhones.Count == 0)
            {
                AllNames = await _historyService.GetAllPreviousNamesAsync();
                AllEgn = await _historyService.GetAllPreviousEGNAsync();
                AllPhones = await _historyService.GetAllPreviousPhonesAsync();
            }

            bool granted = await _smsPermissionService.EnsureSmsPermissionAsync();
            if (!granted)
            {
                await DisplayAlert("Разрешение отказано", "Без достъп до SMS, приложението няма да работи напълно.", "OK");
                return;
            }

            try
            {
                LoadingOverlay.IsVisible = true;
                await SmsRecoveryService.RecoverAsync(); // ще сработи само на Android
            }
            finally
            {
                LoadingOverlay.IsVisible = false;
            }
        }

        private async void ShowMoreInfoForProcedure(object sender, EventArgs e)
        {
            if (sender is StackLayout layout && layout.Children.FirstOrDefault() is Label label && label.Text is string text)
            {
                await DisplayAlert("Пълна информация", text, "Затвори");
            }
        }

        private void OnProcedureCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            UpdateTotalSum();
        }

        private void AutoCompleteText(object sender, TextChangedEventArgs e)
        {
            if (sender is not Entry entry) return;

            var keyword = e.NewTextValue?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                if (entry == CurrentName)
                {
                    NameSuggestions.Clear();
                    AutoCompleteSuggestionsViewVisibility(false, entry);
                }
                else if (entry == EGNEntry)
                {
                    EGNSuggestions.Clear();
                    AutoCompleteSuggestionsViewVisibility(false, entry);
                }
                else if (entry == PhoneEntry)
                {
                    PhoneSuggestions.Clear();
                    AutoCompleteSuggestionsViewVisibility(false, entry);
                }
                return;
            }

            switch (entry)
            {
                case var _ when entry == CurrentName:
                    {
                        var matches = AllNames
                            .Where(n => n.ToLower().StartsWith(keyword))
                            .Take(5)
                            .ToList();

                        NameSuggestions.Clear();
                        foreach (var match in matches)
                            NameSuggestions.Add(match);

                        AutoCompleteSuggestionsViewVisibility(matches.Any(), entry);
                        break;
                    }

                case var _ when entry == EGNEntry:
                    {
                        var matches = AllEgn
                            .Where(n => n.StartsWith(keyword))
                            .Take(5)
                            .ToList();

                        EGNSuggestions.Clear();
                        foreach (var match in matches)
                            EGNSuggestions.Add(match);

                        AutoCompleteSuggestionsViewVisibility(matches.Any(), entry);
                        break;
                    }

                case var _ when entry == PhoneEntry:
                    {
                        var matches = AllPhones
                            .Where(n => n.StartsWith(keyword))
                            .Take(5)
                            .ToList();
                        PhoneSuggestions.Clear();
                        foreach (var match in matches)
                            PhoneSuggestions.Add(match);
                        AutoCompleteSuggestionsViewVisibility(matches.Any(), entry);
                        break;
                    }
            }

        }

        private void AutoCompleteSuggestionsViewVisibility(bool show, VisualElement sender)
        {
            switch (sender)
            {
                case var _ when sender == CurrentName:
                    NameSuggestionsListView.IsVisible = show;
                    break;

                case var _ when sender == EGNEntry:
                    EGNSuggestionsListView.IsVisible = show;
                    break;
                case var _ when sender == PhoneEntry:
                    PhoneSuggestionsListView.IsVisible = show;
                    break;
            }

        }

        private void AutoCompleteSuggestionTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is not string selectedValue) return;

            switch (sender)
            {
                case var _ when sender == NameSuggestionsListView:
                    CurrentName.Text = selectedValue;
                    if (NameSuggestionsListView != null && NameSuggestionsListView.Handler != null)
                    {
                        NameSuggestions.Clear();
                        NameSuggestionsListView.IsVisible = false;
                    }
                    break;

                case var _ when sender == EGNSuggestionsListView:
                    EGNEntry.Text = selectedValue;
                    if (EGNSuggestionsListView != null && EGNSuggestionsListView.Handler != null)
                    {
                        EGNSuggestions.Clear();
                        EGNSuggestionsListView.IsVisible = false;
                    }
                    break;
                case var _ when sender == PhoneSuggestionsListView:
                    PhoneEntry.Text = selectedValue;
                    if (PhoneSuggestionsListView != null && PhoneSuggestionsListView.Handler != null)
                    {
                        PhoneSuggestions.Clear();
                        PhoneSuggestionsListView.IsVisible = false;
                    }
                    break;
            }

        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var selected = CheckedProcedures.Where(p => p.IsSelected).ToList();
            var total = selected.Sum(p => p.Price);

            string name = CurrentName.Text?.Trim()!;
            string egn = EGNEntry.Text?.Trim()!;
            string phone = PhoneEntry.Text?.Trim()!;
            string uin = UIN?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(egn) || string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("Грешка", "Моля, попълни Име, ЕГН и телефонен номер.", "OK");
                return;
            }

            if (egn.Length != 10 || !egn.All(char.IsDigit))
            {
                await DisplayAlert("Грешка", "ЕГН трябва да съдържа точно 10 цифри.", "OK");
                return;
            }

            if (phone.Length != 10 && phone.Length != 13)
            {
                await DisplayAlert("Грешка", "Телефонният номер трябва да съдържа точно 10 или 13 символа", "OK");
                return;
            }

            if (uin.Length != 10 && uin.Length != 0)
            {
                await DisplayAlert("Грешка", "УИН номерът трябва да съдържа точно 10 цифри.", "OK");
                return;
            }

            string line = "--------------------";
            string website = "www.medsestri.com";
            decimal discountTotal = total * 0.8m;


            var manipulationsList = string.Join("\n", selected.Select((p, index) => $"{index + 1}. {p.Name} - {p.Price:F2} лв"));

            var messageBuilder = new StringBuilder();
            messageBuilder.AppendLine($"Пациент: {name}");
            messageBuilder.AppendLine($"ЕГН: {egn}");
            messageBuilder.AppendLine($"Телефон: {phone}");
            messageBuilder.AppendLine();
            messageBuilder.AppendLine($"Избрани манипулации {selected.Count} бр:");
            messageBuilder.AppendLine(manipulationsList);

            if (!string.IsNullOrEmpty(uin))
                messageBuilder.AppendLine($"УИН: {uin}");

            messageBuilder.AppendLine();
            messageBuilder.AppendLine($"Общо сума: {total:F2} лв");
            messageBuilder.AppendLine(line);
            messageBuilder.AppendLine($"Сума с отстъпка: {discountTotal:F2} лв");
            messageBuilder.AppendLine(website);

            string message = messageBuilder.ToString().Trim();

            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = message,
                    Title = "Изпрати чрез Viber"
                });

                var response = await _historyService.AddAsync(new Patient
                {
                    Id = Guid.NewGuid(),
                    FullName = name,
                    Note = message,
                    EGN = egn,
                    PhoneNumber = phone,
                    CreatedAt = DateTime.UtcNow


                });

                if (response.IsSuccessStatusCode)
                {
                    CLearClicked();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Грешка", "Неуспешно записване в историята", "ОК");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Грешка", $"Неуспешно изпращане: {ex.Message}", "OK");
            }

        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            CLearClicked();
        }

        private void CLearClicked()
        {
            CurrentName.Text = "";
            EGNEntry.Text = "";
            PhoneEntry.Text = "";
            UIN.Text = "";

            foreach (var proc in CheckedProcedures.Where(p => p.IsSelected == true))
                proc.IsSelected = false;

            UpdateTotalSum();
        }

        public static async Task<List<MedicalProcedureViewModel>> LoadProceduresAsync()
        {
            var fileName = "procedures.json";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            Stream stream;

            if (!File.Exists(filePath))
            {
                stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            }
            else
            {
                stream = File.OpenRead(filePath);
            }

            using (stream)
            {
                var result = await JsonSerializer.DeserializeAsync<List<MedicalProcedureViewModel>>(stream);
                return result ?? new List<MedicalProcedureViewModel>();
            }
        }

        private async void OnLoadMore(object sender, EventArgs e)
        {
            await LoadMorePaginationProceduresAsync();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            _ = FilterProceduresAsync();
        }

        private CancellationTokenSource _filterCts;

        private async Task FilterProceduresAsync()
        {
            _filterCts?.Cancel();
            _filterCts = new CancellationTokenSource();
            var token = _filterCts.Token;

            try
            {
                await Task.Delay(300, token);
                Procedures.Clear();
                _paginationState.Reset();
                await LoadMorePaginationProceduresAsync();
            }
            catch (TaskCanceledException) { }
        }

        private async Task LoadMorePaginationProceduresAsync()
        {
            if (_paginationState.IsLoading) return;
            _paginationState.IsLoading = true;

            var matching = await GetFilteredProcedureAsync();


            var toAdd = matching.Except(Procedures).ToList();
            foreach (var item in toAdd)
                Procedures.Add(item);

            _paginationState.CurrentIndex += matching.Count;
            _paginationState.IsLoading = false;
        }

        private async Task<List<MedicalProcedureViewModel>> GetFilteredProcedureAsync()
        {
            var keyword = SearchBar.Text?.ToLower() ?? "";
            return await Task.Run(() =>
                CheckedProcedures
                    .Where(p => p.Name.ToLower().Contains(keyword))
                    .Skip(_paginationState.CurrentIndex)
                    .Take(_paginationState.VisibleThreshold)
                    .ToList());
        }

        private void UpdateTotalSum()
        {
            var total = CheckedProcedures.Where(p => p.IsSelected).Sum(p => p.Price);
            TotalLabel.Text = $"{total:F2} лв";
        }

        private async Task LoadAsyncIfNeeded()
        {
            var data = await LoadProceduresAsync();
            CheckedProcedures = new ObservableCollection<MedicalProcedureViewModel>(data);

            ProcedureList.ItemsSource = Procedures;
            await Task.WhenAll(
                FilterProceduresAsync(),
                _historyService.ReadAllPatientsFromCloud());
        }
    }
}
