using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class PagedResponse<TData> : Response<TData> {
    
    [JsonConstructor]
    public PagedResponse(TData? data, int totalCount, int currentPage, int pageSize, int code, string message)
    :base(data, code, message)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
    public PagedResponse(TData? data, int code, string message) : base(data, code, message)
    {
    }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = DefaultConfigurations.DefaultPageSize;
    public int TotalCount { get; set; }
}