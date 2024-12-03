using AutoMapper;
using TodoList_Base.Database;
using TodoList_Base.DTO;

namespace TodoListApp.Mapping
{
    public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}
