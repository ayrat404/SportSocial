namespace BLL.Common.Objects
{
    public class ServiceResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class ServiceResult<T>: ServiceResult
    {
        public T Result { get; set; }
    }
}