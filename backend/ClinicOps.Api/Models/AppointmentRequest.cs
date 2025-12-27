namespace ClinicOps.Api.Models;

public enum AppointmentStatus
{
    Pending,
    Approved,
    Rejected,
    Completed,
    Cancelled
}

public class AppointmentRequest
{
    public int Id { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime PreferredDate { get; set; }
    public string PreferredTime { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? AdminNotes { get; set; }
}
