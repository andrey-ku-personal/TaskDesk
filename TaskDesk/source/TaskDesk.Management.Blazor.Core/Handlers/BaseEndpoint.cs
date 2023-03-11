using System.Net.Http.Json;
using TaskDesk.SharedModel.Error;

namespace TaskDesk.Management.Blazor.Core.Handlers;

public class BaseEndpoint : HttpClient
{
    public async Task<TResponse> Create<TModel, TResponse>(string endpoint, TModel model)
        where TModel : class
        where TResponse : class
    {
        var response = await this.PostAsJsonAsync(endpoint, model);

        return await response.Content.ReadFromJsonAsync<TResponse>() ?? default!;
    }

    public async Task<TResponse> GetAll<TModel, TResponse>(string endpoint, TModel model)
        where TModel : class
        where TResponse : class
    {
        var response = await this.PostAsJsonAsync(endpoint, model);

        return await response.Content.ReadFromJsonAsync<TResponse>() ?? default!;
    }

    private async Task<ApplicationException> GenerateExcepiton(HttpContent httpContent)
    {
        var error = await httpContent.ReadFromJsonAsync<ProblemDetails>();
        var message = $"<ul><li>Error</li><li>{string.Join(',', error!.Errors.Name ?? new())}</li></ul>";
        return new ApplicationException(message);
    }
}
