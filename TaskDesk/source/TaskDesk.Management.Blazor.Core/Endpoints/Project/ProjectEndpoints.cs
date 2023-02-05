using System.Net.Http.Json;
using TaskDesk.Management.Blazor.Core.BaseEnpoints;
using TaskDesk.SharedModel.Filter;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.Blazor.Core.Endpoints.Project;

public class ProjectEndpoints : BaseEndpoint
{
    public ProjectEndpoints()
    {
        BaseAddress = new("https://localhost:44346");
    }

    public async Task<FilteredResult<ProjectViewModel>> GetAll(FilterModel filter)
    {
        var response = await this.PostAsJsonAsync("Project/GetAll", filter);

        return await response.Content.ReadFromJsonAsync<FilteredResult<ProjectViewModel>>() ?? default!;
    }
}