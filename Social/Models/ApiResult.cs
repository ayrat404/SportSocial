namespace Social.Models
{
    public class ApiResult : ApiResultBase
    {

    }
    
    public class ApiResult<T>: ApiResultBase
        where T : class
    {
        public T Data { get; set; }
    }
}