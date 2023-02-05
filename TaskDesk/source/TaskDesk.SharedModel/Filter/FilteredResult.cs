namespace TaskDesk.SharedModel.Filter;

public record FilteredResult<TResult>
    where TResult : class
{
    public FilteredResult(int totalCount, List<TResult> result)
    {
        TotalCount = totalCount;
        Result = result;
    }

    public int TotalCount { get; set; }
    public List<TResult> Result { get; set; }
}