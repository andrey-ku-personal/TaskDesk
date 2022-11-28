namespace TaskDesk.Identity.Handlers.User.Models;

public class UserModel
{
    public  int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public  string Email { get; set; } = default!;
    public DateTime CreateTime { get; set; }
    public DateTime LastLoginTime { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
}