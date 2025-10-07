namespace PersonalFinanceAPI.DTOs.Generics;

public class PagedResponse<T>(List<T> data, int currentPage, int pageSize, int totalRecords)
{
    public List<T> Data { get; set; } = data;
    public int CurrentPage { get; set; } = currentPage;
    public int PageSize { get; set; } = pageSize;
    public int TotalRecords { get; set; } = totalRecords;
    public int TotalPages { get; set; } = (int)Math.Ceiling(totalRecords / (double)pageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
