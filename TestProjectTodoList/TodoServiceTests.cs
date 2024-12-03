using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectTodoList.Interfaces;
using TestProjectTodoList.Services;
using TodoList_Base.Database;


using TodoListApp.Services.Interfaces;

namespace TestProjectTodoList
{
    public class TodoServiceTests
    {
        // We Can make TodoListApp.Repository instead of Mock Repository
        private readonly Mock<IRepository<TodoItem>> _repositoryMock;
        private readonly ITodoService _todoService;

        public TodoServiceTests()
        {
            _repositoryMock = new Mock<IRepository<TodoItem>>();
            _todoService = new TodoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddTodoAsync_ShouldAddTodoAndReturnIt()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Test Todo" };
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(todo);

            // Act
            var result = await _todoService.AddTodoAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Id, result.Id);
            Assert.Equal(todo.Title, result.Title);
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task UpdateTodoAsync_ShouldUpdateAndReturnTodo()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, IsCompleted = true };
            _repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(todo);

            // Act
            var result = await _todoService.UpdateTodoAsync(todo);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todo.Id, result.Id);
            Assert.True(result.IsCompleted);
            _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task GetAllTodosAsync_ShouldReturnAllTodos()
        {
            // Arrange
            var todos = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
    };

            _repositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(todos);

            // Act
            var result = await _todoService.GetAllTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count);
            Assert.Contains(result, t => t.Id == 1);
            Assert.Contains(result, t => t.Id == 2);
            _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPendingTodosAsync_ShouldReturnOnlyPendingTodos()
        {
            // Arrange
            var todos = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
        new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
    };

            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<Func<TodoItem, bool>>()))
                .ReturnsAsync((Func<TodoItem, bool> filter) => todos.Where(filter).ToList());

            // Act
            var result = await _todoService.GetPendingTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.ToList().Count); // Only 2 pending items
            Assert.All(result, t => Assert.False(t.IsCompleted)); // All items should be pending
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<Func<TodoItem, bool>>()), Times.Once);
        }
    }
}
