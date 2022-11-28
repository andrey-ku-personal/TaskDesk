namespace TaskDesk.Identity.Options;

public class GoogleOptions
{
    public string FailedLink { get; set; } = default!;
    public string SuccessLink { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}