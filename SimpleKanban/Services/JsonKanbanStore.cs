using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleKanban.Models;

namespace SimpleKanban.Services
{
    public class JsonKanbanStore : IKanbanStore
    {
        private readonly string _filePath;

        public JsonKanbanStore()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var appFolder = Path.Combine(appData, "SimpleKanban");
            Directory.CreateDirectory(appFolder);
            _filePath = Path.Combine(appFolder, "kanban.json");
        }

        public async Task<BoardState> LoadAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new BoardState();
            }

            try
            {
                using var stream = File.OpenRead(_filePath);
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var json = await reader.ReadToEndAsync();

                var state = JsonSerializer.Deserialize<BoardState>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                });

                if (state is not null)
                {
                    return state;
                }

                var categories = JsonSerializer.Deserialize<ObservableCollection<Category>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                });

                if (categories is not null)
                {
                    return new BoardState { Categories = categories };
                }

                return new BoardState();
            }
            catch (Exception ex)
            {
                var errorPath = Path.Combine(Path.GetDirectoryName(_filePath) ?? string.Empty, "kanban-load-error.txt");
                await File.AppendAllTextAsync(errorPath, $"[{DateTime.Now:O}] Failed to load '{_filePath}': {ex}\n\n");
                return new BoardState();
            }
        }

        public async Task SaveAsync(BoardState state)
        {
            var json = JsonSerializer.Serialize(state, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await File.WriteAllTextAsync(_filePath, json, Encoding.UTF8);
        }
    }
}