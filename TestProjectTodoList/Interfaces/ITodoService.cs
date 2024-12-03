using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList_Base.Database;

namespace TestProjectTodoList.Interfaces
{
    internal interface ITodoService
    {
        Task<TodoItem> AddTodoAsync(TodoItem todo);
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<TodoItem> UpdateTodoAsync(TodoItem todo);
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
    }
}
