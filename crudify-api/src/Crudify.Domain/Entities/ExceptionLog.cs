
namespace Crudify.Domain.Entities;

public record ExceptionLog
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? Path { get; set; }
    public string? QueryString { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
