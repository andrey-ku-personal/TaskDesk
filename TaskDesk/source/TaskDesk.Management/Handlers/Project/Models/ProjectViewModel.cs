using TaskDesk.SharedModel.Enums;

namespace TaskDesk.Management.Handlers.Project.Models;

public class ProjectViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ProjectStatus Status { get; set; }
}
