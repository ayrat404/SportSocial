namespace Social.Models
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public ApiError Errors { get; set; }
        public string Message { get; set; }
    }
    
    public class ApiResult<T>: ApiResult
        where T : class
    {
        public T Data { get; set; }
    }
}