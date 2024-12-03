using System.ComponentModel.DataAnnotations;
using TodoList_Base.Shared.Validation;

namespace TodoList_Base.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        [NotSqlInjecttion]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
