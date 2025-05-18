
namespace Crudify.Domain.Entities;

public record ActivityLog()
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required string Action { get; set; }
    public required string Endpoint { get; set; }
    public required string HttpMethod { get; set; }
    public required string RequestBody { get; set; }
    public required string ResponseBody { get; set; }
    public int StatusCode { get; set; }
    public required string IPAddress { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

