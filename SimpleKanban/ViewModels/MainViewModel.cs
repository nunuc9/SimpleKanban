using System.Collections.ObjectModel;
using System.Linq;
using SimpleKanban.Models;

namespace SimpleKanban.ViewModels
{
    internal class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; } = new();

        public MainViewModel()
        {
            // Seed with typical kanban columns
            Categories.Add(new Category { Name = "Backlog" });
            Categories.Add(new Category { Name = "In Progress" });
            Categories.Add(new Category { Name = "Bugs" });
            Categories.Add(new Category { Name = "Fixed" });

            // Example item
            Categories[0].Items.Add(new KanbanItem
            {
                Title = "Welcome",
                Description = "This is a sample task. Drag me to another column."
            });
        }

        public void AddItemToCategory(Category category, KanbanItem item)
        {
            category.Items.Add(item);
        }

        public void RemoveItem(KanbanItem item)
        {
            var cat = FindCategoryContaining(item);
            if (cat is not null)
            {
                cat.Items.Remove(item);
            }
        }

        public void RemoveCategory(Category category)
        {
            if (Categories.Contains(category))
            {
                Categories.Remove(category);
            }
        }

        public Category? FindCategoryContaining(KanbanItem item) =>
            Categories.FirstOrDefault(c => c.Items.Contains(item));

        public void MoveItemToCategory(KanbanItem item, Category target)
        {
            // Remove from any current category then add to target
            var src = FindCategoryContaining(item);
            if (src is not null)
            {
                if (ReferenceEquals(src, target))
                    return;

                src.Items.Remove(item);
            }

            target.Items.Add(item);
        }
    }
}