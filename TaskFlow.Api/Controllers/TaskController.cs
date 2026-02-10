using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _service.GetAllAsync();
        return Ok(tasks);
    }


    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] TaskFilterRequest filter)
    {
        if (filter.Status is < 0 or > 3)
        {
            return BadRequest("Invalid status value.");
        }

        var tasks = await _service.GetFilteredAsync(filter);
        return Ok(tasks);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok(new { status = "Healthy" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskRequest request)
    {
        var task = await _service.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetAll),
            new { id = task.Id },
            task
        );
    }
}
