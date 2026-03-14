using System;

namespace SimpleKanban.Models
{
    public class KanbanItem : BaseNotify
    {
        public Guid Id { get; } = Guid.NewGuid();

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                RaisePropertyChanged();
            }
        }

        public DateTime CreatedAt { get; } = DateTime.Now;

        public override string ToString() => Title;
    }
}