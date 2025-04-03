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

            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("MedSestriManipulations.Resources.Data.manipulations.txt");
            using StreamReader reader = new(stream);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var parts = line.Split(';');
                if (parts.Length == 2 && decimal.TryParse(parts[1], out var price))
                {
                    var procedure = new MedicalProcedureViewModel
                    {
                        Name = parts[0],
                        Price = price,
                        IsSelected = false
                    };
                    procedure.PropertyChanged += Procedure_PropertyChanged;
                    AllProcedures.Add(procedure);
                }
            //}

            //string[] lines = File.ReadAllLines(".");

            //foreach (var line in lines)
            //{
            //    var parts = line.Split(';');
            //    if (parts.Length == 2 && decimal.TryParse(parts[1], out var price))
            //    {
            //        var procedure = new MedicalProcedureViewModel
            //        {
            //            Name = parts[0],
            //            Price = price,
            //            IsSelected = false
            //        };
            //        procedure.PropertyChanged += Procedure_PropertyChanged;
            //        AllProcedures.Add(procedure);
            //    }

                //}

                //for (int i = 1; i <= 200; i++)
                //{
                //    var procedure = new MedicalProcedureViewModel
                //    {
                //        Name = $"Манипулация {i}",
                //        Price = 10 + i,
                //        IsSelected = false
                //    };

                //    procedure.PropertyChanged += Procedure_PropertyChanged;
                //    AllProcedures.Add(procedure);
                //}

                FilterProcedures();
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

            string firstName = FirstNameEntry.Text?.Trim();
            string lastName = LastNameEntry.Text?.Trim();
            string egn = EGNEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(egn))
            {
                await DisplayAlert("Грешка", "Моля, попълни име, фамилия и ЕГН.", "OK");
                return;
            }

            var message = $"Пациент: {firstName} {lastName}\nЕГН: {egn}\n\nИзбрани манипулации:\n" +
                          string.Join("\n", selected.Select(p => $"{p.Name} - {p.Price} лв")) +
                          $"\n\nОбщо: {total} лв";

            await Clipboard.SetTextAsync(message);

            try
            {
                await Launcher.OpenAsync("viber://");
                //await DisplayAlert("Съобщението е копирано", "Постави текста във Viber чрез 'Paste'.", "OK");
            }
            catch
            {
                await DisplayAlert("Грешка", "Viber не е инсталиран или не може да се отвори.", "OK");
            }
        }

        public class MedicalProcedure
        {
            public int Id { get; set; }
            public string Name { get; set; }
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

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            EGNEntry.Text = string.Empty;

            foreach (var proc in Procedures)
            {
                proc.IsSelected = false;
            }

            UpdateTotal();
        }
    }
}
