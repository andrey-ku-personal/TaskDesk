using TaskDesk.Shared.Enums;

namespace TaskDesk.Identity.Handlers.Token;

public class TokenRequest : IRequest<string>
{
    public GrandType Type { get; set; }
    public string UserIdOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
}
