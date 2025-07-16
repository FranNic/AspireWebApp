namespace AspireWebApp.Shared;

using Microsoft.AspNetCore.Mvc;

public class PaginationParameters : ISearchable
{
    private int _pageSize = 50;

    [FromQuery(Name="pageNumber")]
    public int Page { get; set; } = 0;

    [FromQuery(Name = "pageSize")]
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value;
        }
    }

    [FromQuery(Name = "searchValue")]
    public string? SearchValue { get; set; } = string.Empty;

    
}
public interface ISearchable
{
    public string? SearchValue { get; set; }
}
