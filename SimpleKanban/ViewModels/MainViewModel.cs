using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SimpleKanban.Models;
using SimpleKanban.Services;
using SimpleKanban.Views;

namespace SimpleKanban.ViewModels
{
    internal class MainViewModel
    {
        private readonly IKanbanStore _store;

        public ObservableCollection<Category> Categories { get; } = new();
        public ObservableCollection<Tag> AvailableTags { get; } = new();
        public ICommand AddItemCommand { get; }

        public MainViewModel(IKanbanStore store)
        {
            _store = store;
            AddItemCommand = new RelayCommand<Category>(OnAddItemExecuted);
        }

        public async Task LoadAsync()
        {
            var boardState = await _store.LoadAsync();

            if (boardState.Categories.Any())
            {
                Categories.Clear();
                foreach (var cat in boardState.Categories)
                {
                    Categories.Add(cat);
                }
            }
            else
            {
                Categories.Add(new Category { Name = "Backlog" });
                Categories.Add(new Category { Name = "In Progress" });
                Categories.Add(new Category { Name = "Bugs" });
                Categories.Add(new Category { Name = "Fixed" });
            }

            if (boardState.AvailableTags.Any())
            {
                AvailableTags.Clear();
                foreach (var tag in boardState.AvailableTags)
                {
                    AvailableTags.Add(tag);
                }
            }
            else
            {
                AvailableTags.Add(new Tag { Name = "Bug", Color = "#FFFF6B6B" });
                AvailableTags.Add(new Tag { Name = "Feature", Color = "#FF4ECDC4" });
                AvailableTags.Add(new Tag { Name = "Documentation", Color = "#FFFFE66D" });
                AvailableTags.Add(new Tag { Name = "Task", Color = "#FF95E1D3" });
                AvailableTags.Add(new Tag { Name = "Urgent", Color = "#FFA8E6CF" });
            }

            if (!Categories.Any(c => c.Items.Any()))
            {
                Categories[0].Items.Add(new KanbanItem
                {
                    Title = "Welcome",
                    Description = "This is a sample task.\nDrag me to another column.\nDouble-click to edit."
                });
            }
        }

        public async Task SaveAsync()
        {
            var boardState = new BoardState
            {
                Categories = Categories,
                AvailableTags = AvailableTags
            };
            await _store.SaveAsync(boardState);
        }

        private void SaveStateAsync()
        {
            _ = SaveAsync();
        }

        private void OnAddItemExecuted(Category category)
        {
            var window = new AddItemWindow(AvailableTags)
            {
                Owner = Application.Current.MainWindow
            };

            if (window.ShowDialog() == true && window.Result is not null)
            {
                AddItemToCategory(category, window.Result);
            }
        }

        public void AddItemToCategory(Category category, KanbanItem item)
        {
            category.Items.Add(item);
            SaveStateAsync();
        }

        public void RemoveItem(KanbanItem item)
        {
            var cat = FindCategoryContaining(item);
            if (cat is not null)
            {
                cat.Items.Remove(item);
                SaveStateAsync();
            }
        }

        public void RemoveCategory(Category category)
        {
            if (Categories.Contains(category))
            {
                Categories.Remove(category);
                SaveStateAsync();
            }
        }

        public void SynchronizeTags()
        {
            foreach (var category in Categories)
            {
                foreach (var item in category.Items)
                {
                    for (var i = 0; i < item.Tags.Count; i++)
                    {
                        var tag = item.Tags[i];
                        var definition = AvailableTags.FirstOrDefault(t => t.Id == tag.Id);
                        if (definition is not null)
                        {
                            tag.Name = definition.Name;
                            tag.Color = definition.Color;
                        }
                    }
                }
            }
        }

        public Category? FindCategoryContaining(KanbanItem item) =>
            Categories.FirstOrDefault(c => c.Items.Contains(item));

        public void MoveItemToCategory(KanbanItem item, Category target)
        {
            var src = FindCategoryContaining(item);
            if (src is not null)
            {
                if (ReferenceEquals(src, target))
                    return;

                src.Items.Remove(item);
            }

            target.Items.Add(item);
            SaveStateAsync();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T>? _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke((T)parameter) != false;

        public void Execute(object? parameter) => _execute((T)parameter!);

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}