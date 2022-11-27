namespace TaskDesk.Shared.Queries.Intefaces;

public interface IPageFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}