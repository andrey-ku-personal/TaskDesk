namespace TaskDesk.SharedModel.Error;

public class ProblemDetails
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public Error Errors { get; set; } = default!;
}

public class Error
{
    public List<string>? Name { get; set; }
    public List<string>? Description { get; set; }
}