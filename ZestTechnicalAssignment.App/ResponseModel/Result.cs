namespace ZestTechnicalAssignment.App.ResponseModel
{
    public class Result<T>
    {
        public Result() { }
        public Result(bool success, T? data, int code)
        {
            Success = success;
            StatusCode = code;
            Data = data;
        }
        public Result(bool success, string? message, int code)
        {
            Success = success;
            ErrorMessage = message;
            StatusCode = code;
        }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public static Result<T> Successs(T data) => new(true, data, 200);
        public static Result<T> Failure(string error) => new(false, error, 200);
    }
}
