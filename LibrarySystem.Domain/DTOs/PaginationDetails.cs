namespace LibrarySystem.Domain.DTOs;
public class PaginationDetails
{
    public int TotalPages { get; set; }
    public int NoOfRecords { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }

}