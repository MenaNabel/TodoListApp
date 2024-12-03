using System.ComponentModel.DataAnnotations;

namespace TodoList_Base.Database
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Default: current date    
    }
}
