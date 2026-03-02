using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        var created = await _todoService.CreateAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TodoItem item)
    {
        var updated = await _todoService.UpdateAsync(id, item);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _todoService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}