using System.ComponentModel;

namespace MedSestriManipulations.Models
{
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
