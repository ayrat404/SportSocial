namespace BLL.Common.Objects
{
    public class ServiceResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public static ServiceResult SuccessResult()
        {
            return new ServiceResult()
            {
                Success = true
            };
        }

        public static ServiceResult ErrorResult(string errorMsg)
        {
            return new ServiceResult()
            {
                Success = false,
                ErrorMessage = errorMsg
            };
        }
    }

    public class ServiceResult<T>: ServiceResult
    {
        public T Result { get; set; }

        public static ServiceResult<T> SuccessResult(T result)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Result = result
            };
        }
    }
}