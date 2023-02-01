using System.Net.Http.Json;
using TaskDesk.SharedModel.Error;

namespace TaskDesk.Management.Blazor.Core.BaseEnpoints;

public class BaseEndpoint : HttpClient
{
    public async Task<TResponse> Create<TModel, TResponse>(string endpoint, TModel model)
        where TModel : class
        where TResponse : class
    {
        var response = await this.PostAsJsonAsync(endpoint, model);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            var message = $"<ul><li>Error</li><li>{string.Join(',', error!.Errors.Name ?? new())}</li></ul>";

            throw new ApplicationException(message);
        }

        return await response.Content.ReadFromJsonAsync<TResponse>() ?? default!;
    }
}
