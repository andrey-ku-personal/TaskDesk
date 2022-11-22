using Microsoft.AspNetCore.Identity;

namespace TaskDesk.Domain.Entities;

public class User : IdentityUser<int>
{
    public override int Id { get; set; }
    public override string? UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public override string? Email { get; set; } = default!;
    public override string? PasswordHash { get; set; } = default!;
    public DateTime CreateTime { get; set; }
    public DateTime LastLoginTime { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
}
