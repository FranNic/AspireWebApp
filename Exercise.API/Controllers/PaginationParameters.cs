namespace Exercises.API.Controllers;

using static Exercises.API.Controllers.PaginationParameters;

public class PaginationParameters : ISearchable
{
    private int _pageSize = 50;

    public int StartIndex { get; set; }

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

    public string SearchValue { get; set; } = string.Empty;

    public interface ISearchable
    {
        public string SearchValue { get; set; }
    }
}
