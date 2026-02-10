using TaskFlow.Application.DTOs;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync()
    {
        var tasks = await _repository.GetAllAsync();
        return tasks.Select(MapToResponse);
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);
        return task is null ? null : MapToResponse(task);
    }

    public async Task<IEnumerable<TaskResponse>> GetFilteredAsync(TaskFilterRequest filter)
    {
        var tasks = await _repository.GetFilteredAsync(
            filter.Status,
            filter.DueBefore
        );

        return tasks.Select(MapToResponse);
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(task);
        return MapToResponse(task);
    }

    private static TaskResponse MapToResponse(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt
        };
    }
}
