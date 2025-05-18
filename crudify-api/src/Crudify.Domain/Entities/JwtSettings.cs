
namespace Crudify.Domain.Entities;

public class JwtSettings
{
    public string SecretKey { get; set; } = string.Empty;
    public int TokenExpiry { get; set; }
}
