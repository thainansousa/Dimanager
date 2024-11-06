using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<TData> {
    private readonly int _code = DefaultConfigurations.DefaultStatusCode;
    public TData? Data { get; set; }
    public string Message { get; set; } = string.Empty;

    [JsonConstructor]
    public Response(){
        _code = DefaultConfigurations.DefaultStatusCode;
    }
    public Response(TData? data, int code, string message){
        Data = data;
        Message = message;
        _code = code;
    }
    
    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}