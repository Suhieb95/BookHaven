namespace LibrarySystem.Domain.DTOs;
public class PaginationParam
{
     private string? _searchParam;
     public PaginationParam()
     {
          PageNo = 1;
          PageSize = 10;
          SearchParam = "";
     }
     public PaginationParam(int pageNo, int pageSize, string searchParam = "")
     {
          PageNo = pageNo;
          PageSize = pageSize;
          SearchParam = searchParam;
     }
     public int PageNo { get; set; }
     public int PageSize { get; set; }
     public string? SearchParam
     {
          get => _searchParam;
          set => _searchParam = value?.Trim() ?? "";
     }
}
