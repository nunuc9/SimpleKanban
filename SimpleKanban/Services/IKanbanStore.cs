using System.Threading.Tasks;
using SimpleKanban.Models;

namespace SimpleKanban.Services
{
    public interface IKanbanStore
    {
        Task<BoardState> LoadAsync();
        Task SaveAsync(BoardState state);
    }
}