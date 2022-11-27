using TaskDesk.Shared.Queries.Intefaces;

namespace TaskDesk.Shared.Queries.Filter;

public record FilterModel : IPageFilter
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "Id";
    public bool IsAscending { get; set; }
    public string? SearchByText { get; set; }
}