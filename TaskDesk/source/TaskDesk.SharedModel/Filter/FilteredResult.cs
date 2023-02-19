namespace TaskDesk.SharedModel.Filter;

public class FilteredResult<TResult>
    where TResult : class
{
    public FilteredResult()
    {

    }

    public FilteredResult(int pageNumber, int pageSize, int totalCount, List<TResult> result)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Result = result;
    }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int PageCount
    {
        get
        {
            if (TotalCount > 0)
            {
                if (PageSize <= 0)
                    PageSize = TotalCount;

                return (int)Math.Ceiling(TotalCount / (double)PageSize);
            }

            return 0;
        }
    }

    public List<TResult> Result { get; set; } = default!;
}