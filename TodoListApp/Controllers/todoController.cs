using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.Interfaces;
using TodoList_Base.DTO;

namespace TodoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todoController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;


        public todoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // GET: api/todo
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemsPending()
        {
            var todoItemDTOs = await _todoItemService.GetAllTodoPendingItems();
            if (todoItemDTOs != null)
                return todoItemDTOs.ToList();
            else
                return NotFound();
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItemDTOs = await _todoItemService.GetAllTodoItems();
            if (todoItemDTOs != null)
                return todoItemDTOs.ToList();
            else
                return NotFound();
        }


        // PUT: api/todo/5
        [HttpPut("complete" + "/" + "{id}")]
        public async Task<IActionResult> PutTodoItem(int id, bool isCompleted)
        {
            if (ModelState.IsValid)
            {
                var todoItemDTO = new TodoItemDTO() { IsCompleted = isCompleted };
                var result = await _todoItemService.UpdateTodoItem(id, todoItemDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else return NotFound(result);
            }
            return BadRequest(ModelState);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult> PostTodo(TodoItemDTO todoItemDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _todoItemService.AddTodoItem(todoItemDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll(int index = 0, int size = 20)
        //{
        //    return Ok(await _todoItemService.GetAll(index, size));
        //}
    }
}