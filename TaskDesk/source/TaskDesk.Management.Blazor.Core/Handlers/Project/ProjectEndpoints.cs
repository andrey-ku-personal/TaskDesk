using System.Net.Http.Json;
using TaskDesk.Management.Blazor.Core.Handlers.Project.Models;
using TaskDesk.SharedModel.Filter;

namespace TaskDesk.Management.Blazor.Core.Handlers.Project;

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