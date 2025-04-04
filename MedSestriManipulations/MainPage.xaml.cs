using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace MedSestriManipulations
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<MedicalProcedureViewModel> AllProcedures = new();
        ObservableCollection<MedicalProcedureViewModel> Procedures = new();

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object? sender, EventArgs e)
        {
            await LoadProceduresAsync();
        }

        private async Task LoadProceduresAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Resources/Data/manipulations.txt");
                using StreamReader reader = new(stream);

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var parts = line!.Split(';');

                    if (parts.Length == 2 && decimal.TryParse(parts[1], out var price))
                    {
                        var procedure = new MedicalProcedureViewModel
                        {
                            Name = $"{parts[0]} -",
                            Price = price,
                            IsSelected = false
                        };
                        procedure.PropertyChanged += Procedure_PropertyChanged!;
                        AllProcedures.Add(procedure);
                    }
                }

                FilterProcedures();
            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert("Грешка", ex.Message, "OK");
                await this.DisplayAlert("Грешка", ex.Message, "OK");

            }
        }

        private void Procedure_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MedicalProcedureViewModel.IsSelected))
            {
                UpdateTotal();
            }
        }

        
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProcedures();
        }

        private void FilterProcedures()
        {
            var keyword = SearchBar.Text?.ToLower() ?? "";
            Procedures.Clear();
            foreach (var proc in AllProcedures)
            {
                if (proc.Name.ToLower().Contains(keyword))
                    Procedures.Add(proc);
            }
            ProcedureList.ItemsSource = Procedures;
        }

        private void UpdateTotal()
        {
            var total = Procedures.Where(p => p.IsSelected).Sum(p => p.Price);
            TotalLabel.Text = $"Общо: {total} лв";
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var selected = Procedures.Where(p => p.IsSelected).ToList();
            var total = selected.Sum(p => p.Price);

            string firstName = FirstNameEntry.Text?.Trim()!;
            string lastName = LastNameEntry.Text?.Trim()!;
            string egn = EGNEntry.Text?.Trim()!;
            string phone = PhoneEntry.Text?.Trim()!;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(egn) || string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("Грешка", "Моля, попълни име, фамилия и ЕГН.", "OK");
                return;
            }

            if (egn.Length != 10 || !egn.All(char.IsDigit))
            {
                await DisplayAlert("Грешка", "ЕГН трябва да съдържа точно 10 цифри.", "OK");
                return;
            }

            if (phone.Length != 10 && phone.Length != 13)
            {
                await DisplayAlert("Грешка", "Телефонният номер трябва да съдържа 10 или 13 символа", "OK");
                return;
            }

            var message = $"Пациент: {firstName} {lastName}\n ЕГН: {egn}\n Телефонен номер: {phone}\n\nИзбрани манипулации:\n" +
                          string.Join("\n \n", selected.Select(p => $"{p.Name} - {p.Price} лв")) +
                          $"\n\nОбщо: {total} лв \n Сума с отсъпка:{(total * 0.8m):F2} лв";

            await Clipboard.SetTextAsync(message);

            try
            {
                await Clipboard.SetTextAsync(message);

                await DisplayAlert(
                    "Съобщението е копирано",
                    "Текстът е копиран в клипборда.\n\nОтвори Viber и го постави ръчно (Paste) в чата.",
                    "OK");
            }
            catch
            {
                await DisplayAlert("Грешка", "Viber не е инсталиран или не може да се отвори.", "OK");
            }
        }

        public class MedicalProcedure
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public decimal Price { get; set; }
        }

        public class MedicalProcedureViewModel : MedicalProcedure, INotifyPropertyChanged
        {
            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            EGNEntry.Text = string.Empty;
            PhoneEntry.Text = string.Empty;

            foreach (var proc in Procedures)
            {
                proc.IsSelected = false;
            }

            UpdateTotal();
        }
    }
}
