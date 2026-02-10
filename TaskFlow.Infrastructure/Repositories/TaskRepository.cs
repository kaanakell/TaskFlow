using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure.Persistence;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskFlowDbContext _context;

    public TaskRepository(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredAsync(
        int? status,
        DateTime? dueBefore)
    {
        IQueryable<TaskItem> query = _context.Tasks.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(t => (int)t.Status == status.Value);
        }

        if (dueBefore.HasValue)
        {
            query = query.Where(t => t.DueDate != null && t.DueDate < dueBefore);
        }

        return await query
        .OrderBy(t => t.CreatedAt)
        .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
    }


    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }
}