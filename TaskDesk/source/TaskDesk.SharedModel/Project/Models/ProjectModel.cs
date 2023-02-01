namespace TaskDesk.SharedModel.Project.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}