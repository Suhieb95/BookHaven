namespace LibrarySystem.Domain.DTOs;
public class PaginatedResponse<T>
{
    public IReadOnlyList<T>? Data { get; set; }
    public PaginationDetails PaginationDetails { get; set; } = default!;
    public void SetTotalPage(int pageSize)
      => PaginationDetails.TotalPages = Convert.ToInt16(Math.Ceiling((double)PaginationDetails.NoOfRecords / pageSize));
}
