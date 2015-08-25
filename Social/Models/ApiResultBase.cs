namespace Social.Models
{
    public class ApiResultBase
    {
        public bool Success { get; set; }
        public ApiError Errors { get; set; }
        public string Message { get; set; }
    }
}