using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleKanban.Models
{
    // Abstract class to be inherited by model classes
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Event handler for property changes - ? means nullable

        // Helper method for PropertyChanged event
        // Protected so it needs be called from derived classes
        // If propertyName is not provided, it will use the name of the caller property
        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}