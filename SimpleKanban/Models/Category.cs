using System;
using System.Collections.ObjectModel;

namespace SimpleKanban.Models
{
    public class Category : BaseNotify
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

        public ObservableCollection<KanbanItem> Items { get; set; } = new();
    }
}