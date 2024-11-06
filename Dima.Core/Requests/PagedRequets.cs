namespace Dima.Core.Requests;

public abstract class PagedRequest : Requests {
    public int PageNumber { get; set; } = DefaultConfigurations.DefaultPageNumber;
    public int PageSize { get; set; } = DefaultConfigurations.DefaultPageSize;
}