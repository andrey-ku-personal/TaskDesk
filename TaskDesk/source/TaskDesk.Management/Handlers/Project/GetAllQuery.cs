using System.Linq.Expressions;
using TaskDesk.Shared.Queries;

namespace TaskDesk.Management.Handlers.Project;

public class GetAllQuery : BaseSortQuery<Domain.Entities.Project>
{
    public string? SearchByText { get; set; }

    public override Expression<Func<Domain.Entities.Project, bool>> GetExpression()
    {
        var filter = base.GetExpression();

        if (!string.IsNullOrWhiteSpace(SearchByText))
        {
            foreach (var word in SearchByText.Split())
                filter = filter.And(x => x.Name.Contains(word) || x.Description.Contains(word));
        }

        return filter;
    }
}