namespace CREL_BE.Helpers;

public class PaginationHeader
{
    public PaginationHeader(IPagedList pagedList)
    {
        CurrentPage = pagedList.CurrentPage;
        TotalPages = pagedList.TotalPages;
        ItemsPerPage = pagedList.PageSize;
        TotalItems = pagedList.TotalCount;
    }

    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
