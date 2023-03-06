using System.Linq.Expressions;
using TaskDesk.Shared.Extensions;
using TaskDesk.Shared.Queries;

namespace TaskDesk.Management.Handlers.Project;

public class GetAllQuery : BaseSortQuery<Domain.Entities.Project>
{
    public string? SearchByText { get; set; }
    public ProjectFilter Filter { get; set; } = default!;

    public override Expression<Func<Domain.Entities.Project, bool>> GetExpression()
    {
        var filter = base.GetExpression();

        if (!string.IsNullOrWhiteSpace(SearchByText))
        {
            foreach (var word in SearchByText.Split())
                filter = filter.And(x => x.Name.Contains(word) || x.Description.Contains(word));
        }

        if (Filter is null)
            return filter;

        if (Filter.StatusIds is not null && !Filter.StatusIds.IsEmpty())
            filter = filter.And(f => Filter.StatusIds!.Contains(f.Status));

        return filter;
    }
}