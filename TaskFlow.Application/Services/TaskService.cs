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

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem> CreateAsync(string title, string? description)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description
        };

        await _repository.AddAsync(task);
        return task;
    }
}