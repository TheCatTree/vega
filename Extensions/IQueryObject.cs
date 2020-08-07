namespace vega.Extensions
{
    public interface IQueryObject
    {
        string SortBy {get; set;}

        bool IsSortAcending {get; set;}
        int Page {get; set;}
        int PageSize {get; set;}
    }
}