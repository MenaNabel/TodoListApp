using TodoList_Base.Database;
using TodoList_Base.DTO;
using TodoList_Base.Shared;
//using TodoList_Base.Database;

namespace TodoListApp.Services.Interfaces
{
    public interface ITodoItemService : IInsertAsync<TodoItemDTO>, IReadAsync<TodoItemDTO>
    {
        Task<TodoItemDTO> GetTodoItem(int todoItemId);
        Task<UserMangerResonse> AddTodoItem(TodoItemDTO todoItemDTO);
        Task<UserMangerResonse> UpdateTodoItem(int todoItemId, TodoItemDTO todoItemDTO);
        Task<UserMangerResonse> DeleteTodoItem(int todoItemId);

        Task<IEnumerable<TodoItemDTO>> GetAllTodoPendingItems();
        Task<IEnumerable<TodoItemDTO>> GetAllTodoItems();
    }
}
