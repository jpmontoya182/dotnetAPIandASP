using dotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace dotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        protected List<TodoModel> todolist = new List<TodoModel>();
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            logger = _logger;
            var initialTodo = new TodoModel()
            {
                Id = 1000,
                Title = "Learn C#",
                Description = "Gotta start somewhere",
                IsComplete = false
            };
            todolist.Add(initialTodo);
        }

        [HttpGet]
        public IEnumerable<TodoModel> Get()
        {
            return todolist;
        }

        [HttpGet("{id}/")]
        public ActionResult<TodoModel> GetTodoItem(long id)
        {
            var todoItem = todolist.Find(x => x.Id == id);
            if (todoItem != null)
            {
                return todoItem;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public string AddTodoItem(TodoModel todoItem)
        {
            todolist.Add(todoItem);
            return $"Added '{todoItem.Title}' to the list.";
        }

        [HttpDelete("{id}/")]
        public string DeleteTodoItem(long id)
        {
            var todoItem = todolist.Find(x => x.Id == id);
            if (todoItem != null)
            {
                var _title = todoItem.Title;
                todolist.Remove(todoItem);
                return $"Todo Item '{_title}' was successfully deleted.";
            }
            else
            {
                return $"Could not find a todo with id  '{id.ToString()}'.";
            }
        }

    }
}