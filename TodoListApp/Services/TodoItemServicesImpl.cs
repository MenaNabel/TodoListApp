using AutoMapper;
using TodoList_Base.Database;
using TodoList_Base.DTO;
using TodoListApp.Repository;
using TodoListApp.Repository.Paging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoListApp.Services.Interfaces;
using TodoList_Base.Shared;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services
{
    public class TodoItemServicesImpl : ITodoItemService
    {
        private readonly IRepositoryAsync<TodoItem> _repositoryAsync;

        private readonly IDeleteRepository<TodoItem> _deleterepository;
        
        private IServiceProvider _serviceProvider;
        private ITodoItemService _todoItemService;
        private readonly IMapper _mapper;

        public TodoItemServicesImpl(IServiceProvider serviceProvider,
            IRepositoryAsync<TodoItem> repositoryAsync, IDeleteRepository<TodoItem> deleteRepository,
            IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _deleterepository = deleteRepository;
        }

        private ITodoItemService CreateTodoItemServices()
        {
            return _serviceProvider.GetRequiredService<ITodoItemService>();
        }



        async Task<UserMangerResonse> ITodoItemService.AddTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await CreateTodoItemAsync(todoItemDTO);
            if (todoItem != null)
            {
                return new UserMangerResonse("Todo Item is created successfully", true);
            }
            return new UserMangerResonse("TodoItem is not created ", false);
        }

        async Task<TodoItemDTO> IInsertAsync<TodoItemDTO>.Add(TodoItemDTO todoItemDTO)
        {
            TodoItem todoItem = ConvertToTodoItemModel(todoItemDTO);
            TodoItemAddProcess(todoItem);
            // TODO: Add Subtypes for TodoItem
            var modelAdded = await _repositoryAsync.InsertAsync(todoItem);
            return modelAdded?.Entity is null ? null : ConvertToAddTodoItemViewModel(modelAdded.Entity);
        }

        async Task<IEnumerable<TodoItemDTO>> ITodoItemService.GetAllTodoPendingItems() 
        { 
            var todoItems = await _repositoryAsync.GetListAsync(todo =>
            (todo.IsCompleted == false));
            return todoItems.Items.Select(ConvertToTodoItemViewModel);
        }

        async Task<IEnumerable<TodoItemDTO>> ITodoItemService.GetAllTodoItems()
        {
            var todoItems = await _repositoryAsync.GetListAsync();
            return todoItems.Items.Select(ConvertToTodoItemViewModel);
        }

        async Task<TodoItemDTO> ITodoItemService.GetTodoItem(int todoItemId)
        {
            var todoItem = await TodoItemExists(todoItemId);
            var entity = ConvertToTodoItemViewModel(todoItem);
            return entity is null ? null : entity;
        }

        async Task<UserMangerResonse> ITodoItemService.UpdateTodoItem(int todoItemId, TodoItemDTO todoItemDTO)
        {
            var todoItem = await TodoItemExists(todoItemId);
            if(todoItem == null)
                return new UserMangerResonse("You can't edit this todoItem", false);
            var result = TryUpdateTodoItem(todoItem,todoItemDTO);
            if(result)
                return new UserMangerResonse("TodoItem updated successfully", true);
            else
                return new UserMangerResonse("TodoItem not updated", false);
        }

        async Task<UserMangerResonse> ITodoItemService.DeleteTodoItem(int todoItemId)
        {
            var todoItem = await TodoItemExists(todoItemId);
            if (todoItem == null)
                return new UserMangerResonse("You can't delete this todoItem", false);
            try
            {
                TryDeleteTodoItem(todoItem);
                return new UserMangerResonse("TodoItem deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new UserMangerResonse(ex.Message, false);
            }
        }

        public Task<IEnumerable<TodoItemDTO>> AddRange(IEnumerable<TodoItemDTO> Collection)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginate<TodoItemDTO>> GetAll(int index = 0, int Size = 20)
        {
            var todoItems = await _repositoryAsync.GetListAsync(index: index, size: Size);
            return new Paginate<TodoItem, TodoItemDTO>(todoItems, todoItem => todoItem.Select(todo => ConvertToAddTodoItemViewModel(todo)));
        }

        #region Helper

        private TodoItem ConvertToTodoItemModel(TodoItemDTO todoItemDTO)
        {
            return _mapper.Map<TodoItem>(todoItemDTO);
        }

        private TodoItemDTO ConvertToAddTodoItemViewModel(TodoItem todoItem)
        {
            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        private TodoItemDTO ConvertToTodoItemViewModel(TodoItem todoItem)
        {
            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        private void TodoItemAddProcess(TodoItem todoItem)
        {
            todoItem.Id = 0;
            //if (todoItem.CreatedDate == null)
            //    todoItem.CreatedDate = DateTime.Now;
        }

        private async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            _todoItemService = CreateTodoItemServices();

            return await _todoItemService.Add(todoItemDTO);
        }

        private async Task<TodoItem> TodoItemExists(int todoItemId)
        {
            try
            {
                var todoItem = await _repositoryAsync.
                    SingleOrDefaultAsync(s => s.Id == todoItemId);
                return todoItem is not null ? todoItem : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool TryUpdateTodoItem(TodoItem todoItem, TodoItemDTO todoItemDTO)
        {
            todoItem.IsCompleted = todoItemDTO.IsCompleted;
            var result = _repositoryAsync.Update(todoItem);
            return result;
        }

        private void TryDeleteTodoItem(TodoItem todoItem)
        {
            try
            {
                _deleterepository.Delete(todoItem);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public Task<IEnumerable<PortViewModel>> AddRange(IEnumerable<PortViewModel> Collection)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IPaginate<TodoItemDTO>> IReadAsync<TodoItemDTO>.GetAll(int index, int Size)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<AddServiceViewModel>> AddRange(IEnumerable<AddServiceViewModel> Collection)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
