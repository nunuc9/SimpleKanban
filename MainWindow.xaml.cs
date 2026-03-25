using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SimpleKanban.Models;
using SimpleKanban.ViewModels;
using SimpleKanban.Views;

namespace SimpleKanban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point _dragStartPoint;
        private ListBox? _sourceListBox;

        public MainWindow()
        {
            InitializeComponent();

            // Ensure double-clicks on ListBoxItem open the editor.
            // handledEventsToo = true allows us to receive the double-click even if a child control marked it handled.
            EventManager.RegisterClassHandler(
                typeof(ListBoxItem),
                UIElement.MouseDoubleClickEvent,
                new MouseButtonEventHandler(ListBoxItem_MouseDoubleClick),
                handledEventsToo: true);
        }

        private MainViewModel ViewModel => (MainViewModel)DataContext!;

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Category cat)
            {
                var dlg = new AddItemWindow { Owner = this };
                if (dlg.ShowDialog() == true && dlg.Result is not null)
                {
                    ViewModel.AddItemToCategory(cat, dlg.Result);
                }
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is KanbanItem item)
            {
                var dlg = new AddItemWindow(item) { Owner = this };
                if (dlg.ShowDialog() == true && dlg.Result is not null)
                {
                    // If editing we updated the original object directly, nothing more
                }
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is KanbanItem item)
            {
                var result = MessageBox.Show(this, $"Delete \"{item.Title}\"?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.RemoveItem(item);
                }
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Category cat)
            {
                var result = MessageBox.Show(this, $"Delete category \"{cat.Name}\" and all its items?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.RemoveCategory(cat);
                }
            }
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
            _sourceListBox = sender as ListBox;
        }

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            var currentPosition = e.GetPosition(null);
            var diff = currentPosition - _dragStartPoint;

            if (Math.Abs(diff.X) < SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(diff.Y) < SystemParameters.MinimumVerticalDragDistance)
                return;

            if (_sourceListBox?.SelectedItem is KanbanItem item)
            {
                var data = new DataObject(typeof(KanbanItem), item);
                DragDrop.DoDragDrop(_sourceListBox, data, DragDropEffects.Move);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(KanbanItem)))
                return;

            if (sender is not ListBox targetListBox)
                return;

            var item = (KanbanItem?)e.Data.GetData(typeof(KanbanItem));

            if (item is null)
                return;

            // Find target category from DataContext of the ListBox's parent Item container.
            if (targetListBox.DataContext is Category targetCategory)
            {
                ViewModel.MoveItemToCategory(item, targetCategory);
            }
        }

        // Class-level handler invoked when user double-clicks a ListBoxItem.
        // Uses the AddItemWindow constructor that accepts an existing item (so Delete button is visible).
        private void ListBoxItem_MouseDoubleClick(object? sender, MouseButtonEventArgs e)
        {
            if (sender is not ListBoxItem listBoxItem)
                return;

            if (listBoxItem.DataContext is not KanbanItem item)
                return;

            var dlg = new AddItemWindow(item) { Owner = this };

            if (dlg.ShowDialog() == true)
            {
                if (dlg.DeleteRequested)
                {
                    ViewModel.RemoveItem(item);
                }
                else
                {
                    // Edited in place no further action required.
                }
            }

            // Mark handled so other handlers don't also process it.
            e.Handled = true;
        }
    }
}