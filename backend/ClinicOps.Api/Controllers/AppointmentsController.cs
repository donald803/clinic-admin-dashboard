using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicOps.Api.Data;
using ClinicOps.Api.DTOs;
using ClinicOps.Api.Models;

namespace ClinicOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ClinicOpsDbContext _context;

    public AppointmentsController(ClinicOpsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<AppointmentRequest>>> GetAppointments(
        [FromQuery] string? status = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.AppointmentRequests.AsQueryable();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<AppointmentStatus>(status, true, out var statusEnum))
        {
            query = query.Where(a => a.Status == statusEnum);
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderByDescending(a => a.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = new PaginatedResult<AppointmentRequest>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentRequest>> GetAppointment(int id)
    {
        var appointment = await _context.AppointmentRequests.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        return Ok(appointment);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentRequest>> CreateAppointment(CreateAppointmentDto dto)
    {
        var appointment = new AppointmentRequest
        {
            PatientName = dto.PatientName,
            Email = dto.Email,
            Phone = dto.Phone,
            PreferredDate = dto.PreferredDate,
            PreferredTime = dto.PreferredTime,
            Reason = dto.Reason,
            Status = AppointmentStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _context.AppointmentRequests.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateAppointmentStatus(int id, UpdateAppointmentStatusDto dto)
    {
        var appointment = await _context.AppointmentRequests.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        if (Enum.TryParse<AppointmentStatus>(dto.Status, true, out var status))
        {
            appointment.Status = status;
        }

        if (!string.IsNullOrEmpty(dto.AdminNotes))
        {
            appointment.AdminNotes = dto.AdminNotes;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
