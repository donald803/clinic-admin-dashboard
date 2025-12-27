using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicOps.Api.Data;
using ClinicOps.Api.DTOs;
using TaskStatus = ClinicOps.Api.Models.TaskStatus;
using TaskItem = ClinicOps.Api.Models.TaskItem;
using TaskPriority = ClinicOps.Api.Models.TaskPriority;

namespace ClinicOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ClinicOpsDbContext _context;

    public TasksController(ClinicOpsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<TaskItem>>> GetTasks(
        [FromQuery] string? status = null,
        [FromQuery] string? priority = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<TaskStatus>(status, true, out var statusEnum))
        {
            query = query.Where(t => t.Status == statusEnum);
        }

        if (!string.IsNullOrEmpty(priority) && Enum.TryParse<TaskPriority>(priority, true, out var priorityEnum))
        {
            query = query.Where(t => t.Priority == priorityEnum);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PaginatedResult<TaskItem>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(CreateTaskDto dto)
    {
        if (!Enum.TryParse<TaskPriority>(dto.Priority, true, out var priority))
        {
            priority = TaskPriority.Medium;
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = priority,
            DueDate = dto.DueDate,
            Status = TaskStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(dto.Title))
        {
            task.Title = dto.Title;
        }

        if (!string.IsNullOrEmpty(dto.Description))
        {
            task.Description = dto.Description;
        }

        if (!string.IsNullOrEmpty(dto.Status) && Enum.TryParse<TaskStatus>(dto.Status, true, out var status))
        {
            task.Status = status;
            if (status == TaskStatus.Completed && task.CompletedAt == null)
            {
                task.CompletedAt = DateTime.UtcNow;
            }
        }

        if (!string.IsNullOrEmpty(dto.Priority) && Enum.TryParse<TaskPriority>(dto.Priority, true, out var priority))
        {
            task.Priority = priority;
        }

        if (dto.DueDate.HasValue)
        {
            task.DueDate = dto.DueDate;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
