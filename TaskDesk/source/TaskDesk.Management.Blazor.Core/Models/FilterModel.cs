namespace TaskDesk.Management.Blazor.Core;

public class FilterModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public bool IsAscending { get; set; }
    public string? SearchByText { get; set; }
}