namespace LibrarySystem.Domain.DTOs;
public class PaginationDetails
{
    public int TotalPages { get; set; }
    public int NoOfRecords { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

}