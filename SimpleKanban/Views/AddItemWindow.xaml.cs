using System.Windows;
using SimpleKanban.Models;

namespace SimpleKanban.Views
{
    public partial class AddItemWindow : Window
    {
        private readonly KanbanItem? _editingItem;

        public KanbanItem? Result { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();
            Title = "Add Item";
        }

        public AddItemWindow(KanbanItem editingItem) : this()
        {
            _editingItem = editingItem;
            Title = "Edit Item";

            // populate fields
            TitleTextBox.Text = editingItem.Title;
            DescriptionTextBox.Text = editingItem.Description;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show(this, "Title is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editingItem is not null)
            {
                // update existing
                _editingItem.Title = title;
                _editingItem.Description = DescriptionTextBox.Text?.Trim() ?? string.Empty;
                Result = _editingItem;
            }
            else
            {
                Result = new KanbanItem
                {
                    Title = title,
                    Description = DescriptionTextBox.Text?.Trim() ?? string.Empty
                };
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}