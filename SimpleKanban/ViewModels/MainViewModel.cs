using System.Collections.ObjectModel;
using System.Linq;
using SimpleKanban.Models;
using System.Windows.Input;
using System.Windows;
using SimpleKanban.Views;

namespace SimpleKanban.ViewModels
{
    internal class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; } = new();
        public ICommand AddItemCommand { get; }

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
                Description = "This is a sample task.\nDrag me to another column.\nDouble-click to edit."
            });

            AddItemCommand = new RelayCommand<Category>(OnAddItemExecuted);
        }

        private void OnAddItemExecuted(Category category)
        {
            var window = new AddItemWindow
            {
                Owner = Application.Current.MainWindow
            };

            if (window.ShowDialog() == true)
            {
                var item = new KanbanItem
                {
                    Title = window.TitleTextBox.Text,
                    Description = window.DescriptionTextBox.Text
                };

                AddItemToCategory(category, item);
            }
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

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) != false;

        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}