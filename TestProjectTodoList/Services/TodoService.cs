using TestProjectTodoList.Interfaces;
using TodoList_Base.Database;

namespace TestProjectTodoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _repository;

        public TodoService(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> AddTodoAsync(TodoItem todo)
        {
            return await _repository.AddAsync(todo);
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoItem> UpdateTodoAsync(TodoItem todo)
        {
            return await _repository.UpdateAsync(todo);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
        {
            return await _repository.GetAllAsync(t => !t.IsCompleted);
        }
    }
}
