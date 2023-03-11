namespace TaskDesk.Management.Blazor.Core.Handlers.Project.Models;

public class ProjectViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Status { get; set; } = default!;
}
