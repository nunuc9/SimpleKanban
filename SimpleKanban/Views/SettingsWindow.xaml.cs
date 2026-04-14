using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SimpleKanban.Models;
using SimpleKanban.ViewModels;

namespace SimpleKanban.Views
{
    internal partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private Category? _selectedCategory;
        private Tag? _selectedTag;

        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Tag> AvailableTags { get; }

        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public Tag? SelectedTag
        {
            get => _selectedTag;
            set => SetProperty(ref _selectedTag, value);
        }

        public SettingsWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            Categories = viewModel.Categories;
            AvailableTags = viewModel.AvailableTags;
            DataContext = this;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category { Name = "New Category" };
            Categories.Add(category);
            SelectedCategory = category;
        }

        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory is null)
                return;

            Categories.Remove(SelectedCategory);
            SelectedCategory = Categories.FirstOrDefault();
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            var tag = new Tag { Name = "New Tag", Color = "#FF95E1D3" };
            AvailableTags.Add(tag);
            SelectedTag = tag;
        }

        private void RemoveTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTag is null)
                return;

            AvailableTags.Remove(SelectedTag);
            SelectedTag = AvailableTags.FirstOrDefault();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}