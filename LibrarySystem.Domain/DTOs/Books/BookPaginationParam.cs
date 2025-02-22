    namespace LibrarySystem.Domain.DTOs.Books;
public class BookPaginationParam : PaginationParam
{
    public BookPaginationParam() : base()
    {
        PageNo = 1;
        PageSize = 10;
        SearchParam = "";
        FilterInStock = false;
    }
    public BookPaginationParam(int pageNo, int pageSize, string searchParam = "", bool filterInStock = false)
         : base(pageNo, pageSize, searchParam)
        => FilterInStock = filterInStock;
    public bool FilterInStock { get; set; }
}
