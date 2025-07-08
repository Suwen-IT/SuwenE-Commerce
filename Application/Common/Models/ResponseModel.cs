namespace Application.Common.Models;

public class ResponseModel<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string[]? Messages { get; set; }
    public int StatusCode { get; set; }


    public ResponseModel() { }

    public ResponseModel(T data, int statusCode = 200)
    {
        Success = true;
        Data = data;
        Messages = null;
        StatusCode = statusCode;
    }

    public ResponseModel(string[] messages, int statusCode = 400)
    {
        Success = false;
        Data = default;
        Messages = messages;
        StatusCode = statusCode;
    }

    public ResponseModel(string message, int statusCode = 400)
    {
        Success = false;
        Data = default;
        Messages = new[] { message };
        StatusCode = statusCode;
    }
}