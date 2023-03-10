namespace CREL_BE.Helpers;

public class PaginationParams
{
    private const int MaxPageSize = 1000;
    public int PageNumber { get; set; } = 1;
    private int pageSize = 10;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

}
