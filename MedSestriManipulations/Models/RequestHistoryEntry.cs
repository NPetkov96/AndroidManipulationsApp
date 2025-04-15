using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedSestriManipulations.Models
{
    public class RequestHistoryEntry : INotifyPropertyChanged
    {
        public string Name { get; set; } = "";
        public string Note { get; set; } = "";
        public string EGN { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime Date { get; set; }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
