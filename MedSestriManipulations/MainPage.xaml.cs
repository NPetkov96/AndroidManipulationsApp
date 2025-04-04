using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MedSestriManipulations
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<MedicalProcedureViewModel> AllProcedures = new();
        ObservableCollection<MedicalProcedureViewModel> Procedures = new();

        public MainPage()
        {
            InitializeComponent();
            LoadProcedures();
            FilterProcedures();
        }

        private void LoadProcedures()
        {
            AllProcedures = MedicalProcedureService.GetAllProcedures();
            foreach (var procedure in AllProcedures)
            {
                procedure.PropertyChanged += Procedure_PropertyChanged;
            }

            FilterProcedures();
        }

        private void Procedure_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MedicalProcedureViewModel.IsSelected))
                UpdateTotal();
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

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterProcedures();
        }

        private void UpdateTotal()
        {
            var total = AllProcedures.Where(p => p.IsSelected).Sum(p => p.Price);
            TotalLabel.Text = $"Общо: {total:F2} лв";
        }

        private void OnProcedureCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            UpdateTotal();
        }


        private async void OnSendClicked(object sender, EventArgs e)
        {
            var selected = AllProcedures.Where(p => p.IsSelected).ToList();
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

            var message = $"Пациент: {firstName} {lastName}\nЕГН: {egn}\nТелефон: {phone}\n\n" +
                          $"Избрани манипулации:\n" +
                          string.Join("\n", selected.Select(p => $"{p.Name} - {p.Price} лв")) +
                          $"\n\nОбщо: {total} лв\nСума с отстъпка: {(total * 0.8m):F2} лв";

            try
            {
                await Clipboard.SetTextAsync(message);
                await DisplayAlert("Успешно", "Текстът е копиран. Постави го във Viber.", "OK");
            }
            catch
            {
                await DisplayAlert("Грешка", "Неуспешно копиране. Увери се, че Viber е инсталиран.", "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            FirstNameEntry.Text = "";
            LastNameEntry.Text = "";
            EGNEntry.Text = "";
            PhoneEntry.Text = "";

            foreach (var proc in AllProcedures)
                proc.IsSelected = false;

            UpdateTotal();
        }

        public class MedicalProcedureViewModel : INotifyPropertyChanged
        {
            public string Name { get; set; } = "";
            public decimal Price { get; set; }

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
    }
}
