using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Helpers;

public class PagedList<T> : List<T>, IPagedList
{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    // public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber,
    //     int pageSize)
    // {
    //     var count = await source.CountAsync();
    //     var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    //     return new PagedList<T>(items, count, pageNumber, pageSize);
    // }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, PaginationParams paginationParams)
    {
        var count = await source.CountAsync();
        if (paginationParams.PageSize >= 0){
            source = source.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize).Take(paginationParams.PageSize);
        }
        var items = await source.ToListAsync();
        return new PagedList<T>(items, count, paginationParams.PageNumber, paginationParams.PageSize);
    }
}
