namespace CREL_BE.Helpers;

public interface IPagedList
{
    int CurrentPage { get; set; }
    int TotalPages { get; set; }
    int PageSize { get; set; }
    int TotalCount { get; set; }
}
