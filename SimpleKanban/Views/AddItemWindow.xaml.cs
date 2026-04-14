using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SimpleKanban.Models;

namespace SimpleKanban.Views
{
    public partial class AddItemWindow : Window
    {
        private readonly KanbanItem? _editingItem;

        public KanbanItem? Result { get; private set; }

        /// <summary>
        /// True when the user chose to delete the editing item from within this dialog.
        /// Only meaningful when editing an existing item.
        /// </summary>
        public bool DeleteRequested { get; private set; }

        public AddItemWindow(ObservableCollection<Tag> availableTags)
        {
            InitializeComponent();
            Title = "Add Item";
            DeleteButton.Visibility = Visibility.Collapsed;
            TagsListBox.ItemsSource = availableTags;
        }

        public AddItemWindow(KanbanItem editingItem, ObservableCollection<Tag> availableTags) : this(availableTags)
        {
            _editingItem = editingItem;
            Title = "Edit Item";

            TitleTextBox.Text = editingItem.Title;
            DescriptionTextBox.Text = editingItem.Description;

            foreach (var availableTag in availableTags)
            {
                if (editingItem.Tags.Any(existing => existing.Id == availableTag.Id))
                {
                    TagsListBox.SelectedItems.Add(availableTag);
                }
            }

            DeleteButton.Visibility = Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show(this, "Title is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedTags = TagsListBox.SelectedItems.OfType<Tag>().ToList();
            if (_editingItem is not null)
            {
                // update existing
                _editingItem.Title = title;
                _editingItem.Description = DescriptionTextBox.Text?.Trim() ?? string.Empty;
                _editingItem.Tags.Clear();
                foreach (var tag in selectedTags)
                {
                    _editingItem.Tags.Add(new Tag { Id = tag.Id, Name = tag.Name, Color = tag.Color });
                }
                Result = _editingItem;
            }
            else
            {
                Result = new KanbanItem
                {
                    Title = title,
                    Description = DescriptionTextBox.Text?.Trim() ?? string.Empty,
                    Tags = new System.Collections.ObjectModel.ObservableCollection<Tag>(selectedTags.Select(tag => new Tag { Id = tag.Id, Name = tag.Name, Color = tag.Color }))
                };
            }

            DialogResult = true;
            Close();
        }

        private void TagsListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not System.Windows.Controls.ListBox listBox)
                return;

            if (listBox.SelectedItem is Tag selectedTag)
            {
                if (listBox.SelectedItems.Contains(selectedTag))
                {
                    listBox.SelectedItems.Remove(selectedTag);
                }
                else
                {
                    listBox.SelectedItems.Add(selectedTag);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_editingItem is null)
                return;

            var result = MessageBox.Show(this, $"Delete \"{_editingItem.Title}\"?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                DeleteRequested = true;
                // We do not return a Result when deleting, the caller should check DeleteRequested.
                DialogResult = true;
                Close();
            }
        }
    }
}