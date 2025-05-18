
using System.Text.Json.Serialization;

namespace Crudify.Domain.Entities;

public class Student : BaseEntity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("fullName")]

    public string FullName { get; set; }
    [JsonPropertyName("dob")]
    public DateTime DateOfBirth { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]

    public string PhoneNumber { get; set; }
}
