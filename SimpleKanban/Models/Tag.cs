using System;

namespace SimpleKanban.Models
{
    public class Tag : BaseNotify
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _color = "#FF5B9BD5"; // Default color
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color) return;
                _color = value;
                RaisePropertyChanged();
            }
        }

        public override string ToString() => Name;
    }
}
