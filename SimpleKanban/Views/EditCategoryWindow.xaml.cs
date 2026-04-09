using System.Windows;
using SimpleKanban.Models;

namespace SimpleKanban.Views
{
    public partial class EditCategoryWindow : Window
    {
        public string CategoryName { get; private set; } = string.Empty;

        public EditCategoryWindow(Category category)
        {
            InitializeComponent();
            CategoryNameTextBox.Text = category.Name;
            CategoryNameTextBox.Focus();
            CategoryNameTextBox.SelectAll();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var name = CategoryNameTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(this, "Category name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CategoryName = name;
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
