namespace TaskDesk.Identity.Handlers.Account.Models;

public class UserModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Website { get; set; }
    public string? Description { get; set; }
}