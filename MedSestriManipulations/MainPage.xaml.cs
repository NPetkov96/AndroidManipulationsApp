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

        private async void OnProcedureLabelTapped(object sender, EventArgs e)
        {
            if (sender is StackLayout layout &&
                layout.Children.FirstOrDefault() is Label label &&
                label.Text is string text)
            {
                await DisplayAlert("Пълна информация", text, "Затвори");
            }
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

        private void OnProcedureCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            var total = AllProcedures.Where(p => p.IsSelected).Sum(p => p.Price);
            TotalLabel.Text = $"Общо: {total:F2} лв";
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var selected = AllProcedures.Where(p => p.IsSelected).ToList();
            var total = selected.Sum(p => p.Price);

            string name = CurrentName.Text?.Trim()!;
            string egn = EGNEntry.Text?.Trim()!;
            string phone = PhoneEntry.Text?.Trim()!;
            string uin = UIN?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(egn) || string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("Грешка", "Моля, попълни име, ЕГН, телефонен номер и УИН.", "OK");
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

            if (uin.Length != 10 && uin.Length != 0)
            {
                await DisplayAlert("Грешка", "УИН номерът трябва да съдържа точно 10 цифри.", "OK");
                return;
            }

            string line = "-------------------------";
            int counter = 1;
            string website = "www.medsestri.com";
            var message = string.Empty;
            if (uin == string.Empty)
            {
                message = $"Пациент: {name}\nЕГН: {egn}\nТелефон: {phone}\n\n" +
                          $"Избрани манипулации {selected.Count}бр:\n" +
                          string.Join("\n", selected.Select(p => $"{counter++}.{p.Name} - {p.Price:F2} лв")) +
                          $"\n\nОбщо сума: {total:F2} лв\n{line} \nСума с отстъпка: {(total * 0.8m):F2} лв" +
                          $"\n{website}";
            }
            else
            {
                message = $"Пациент: {name}\nЕГН: {egn}\nТелефон: {phone}\n\n" +
                              $"Избрани манипулации {selected.Count}бр:\n" +
                              string.Join("\n", selected.Select(p => $"{counter++}.{p.Name} - {p.Price:F2} лв")) +
                              $"\nУИН:{uin}" +
                              $"\n\nОбщо сума: {total:F2} лв\n{line} \nСума с отстъпка: {(total * 0.8m):F2} лв" +
                              $"\n{website}";
            }

            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = message,
                    Title = "Изпрати чрез Viber"
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Грешка", $"Неуспешно изпращане: {ex.Message}", "OK");
            }

            //try
            //{
            //    await Clipboard.SetTextAsync(message);
            //    await DisplayAlert("Успешно", "Текстът е копиран. Постави го във Viber.", "OK");
            //}
            //catch
            //{
            //    await DisplayAlert("Грешка", "Неуспешно копиране. Увери се, че Viber е инсталиран.", "OK");
            //}
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            CurrentName.Text = "";
            EGNEntry.Text = "";
            PhoneEntry.Text = "";

            foreach (var proc in AllProcedures)
                proc.IsSelected = false;

            UpdateTotal();
        }

        private void LoadProcedures()
        {
            AllProcedures = MedicalProcedureService.GetAllProcedures();
            foreach (var procedure in AllProcedures)
            {
                procedure.PropertyChanged += Procedure_PropertyChanged!;
            }

            FilterProcedures();
        }
    }
}
