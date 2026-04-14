using System;
using System.Windows;
using SimpleKanban.Services;
using SimpleKanban.ViewModels;

namespace SimpleKanban
{
    public partial class App : Application
    {
        // Store instance for loading/saving data from json file
        // The constructor of JsonKanbanStore gest appdata path and file
        private IKanbanStore _store = new JsonKanbanStore(); 
        private MainViewModel _viewModel;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _viewModel = new MainViewModel(_store); // Provide MainViewModel with the store instance from json file
            await _viewModel.LoadAsync();

            var mainWindow = new MainWindow
            {
                DataContext = _viewModel
            };
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _viewModel.SaveAsync();
            base.OnExit(e);
        }
    }
}