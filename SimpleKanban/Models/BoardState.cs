using System.Collections.ObjectModel;

namespace SimpleKanban.Models
{
    public class BoardState
    {
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<Tag> AvailableTags { get; set; } = new();
    }
}