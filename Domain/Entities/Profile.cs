using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Profile : BaseEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Birthday { get; set; }
}
