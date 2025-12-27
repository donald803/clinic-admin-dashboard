using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicOps.Api.Data;
using ClinicOps.Api.DTOs;
using TaskStatus = ClinicOps.Api.Models.TaskStatus;
using TaskItem = ClinicOps.Api.Models.TaskItem;
using AppointmentStatus = ClinicOps.Api.Models.AppointmentStatus;

namespace ClinicOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ClinicOpsDbContext _context;

    public DashboardController(ClinicOpsDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetStats()
    {
        var today = DateTime.UtcNow.Date;

        var stats = new DashboardStatsDto
        {
            TotalTasks = await _context.Tasks.CountAsync(),
            PendingTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.Pending),
            InProgressTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.InProgress),
            CompletedTasks = await _context.Tasks.CountAsync(t => t.Status == TaskStatus.Completed),
            TotalAppointments = await _context.AppointmentRequests.CountAsync(),
            PendingAppointments = await _context.AppointmentRequests.CountAsync(a => a.Status == AppointmentStatus.Pending),
            ApprovedAppointments = await _context.AppointmentRequests.CountAsync(a => a.Status == AppointmentStatus.Approved),
            TodayAppointments = await _context.AppointmentRequests.CountAsync(a => a.PreferredDate.Date == today)
        };

        return Ok(stats);
    }
}
