using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace MedSestriManipulations.Models
{
    public class Patient : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = "";
        public string Note { get; set; } = "";
        public string EGN { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string LabId { get; set; } = "";
        public string LabPassword { get; set; } = "";
        public DateTime CreatedAt { get; set; }

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

        [JsonIgnore]
        public string CreatedAtLocal => CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm");


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
