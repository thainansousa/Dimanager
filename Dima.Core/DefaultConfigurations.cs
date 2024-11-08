namespace Dima.Core;

public static class DefaultConfigurations {
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;

    public static string ConnectionString {get; set;} = string.Empty;
    public static string BackendURL { get; set; } = string.Empty;
    public static string FrontendURL { get; set; } = string.Empty;
}