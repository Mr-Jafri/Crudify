using System.ComponentModel.DataAnnotations.Schema;

namespace Crudify.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Token { get; set; }
    public string? JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }

    public ApplicationUser User { get; set; }
}
