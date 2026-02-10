namespace TaskFlow.Application.DTOs;

public class TaskFilterRequest
{
    public int? Status { get; set; }
    public DateTime? DueBefore { get; set; }
}