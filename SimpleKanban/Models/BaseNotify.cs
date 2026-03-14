using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleKanban.Models
{
    // Class to be inherited by model classes
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        // 
        public event PropertyChangedEventHandler? PropertyChanged;

        // Helper method for PropertyChanged event - 
        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}