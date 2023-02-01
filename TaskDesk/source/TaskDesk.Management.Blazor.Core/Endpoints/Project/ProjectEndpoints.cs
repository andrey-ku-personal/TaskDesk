using TaskDesk.Management.Blazor.Core.BaseEnpoints;

namespace TaskDesk.Management.Blazor.Core.Endpoints.Project;

public class ProjectEndpoints : BaseEndpoint
{
    public ProjectEndpoints()
    {
        BaseAddress = new("https://localhost:44346");
    }
}