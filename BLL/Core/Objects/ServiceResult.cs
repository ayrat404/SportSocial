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

        public static ServiceResult<T> ErrorResult<T>(string errorMsg)
        {
            return new ServiceResult<T>
            {
                Success = false,
                ErrorMessage = errorMsg
            };
        }
        public static ServiceResult<T> SuccessResult<T>(T result)
        {
            return new ServiceResult<T>
            {
                Success = true,
                Result = result
            };
        }
    }

    public class ServiceResult<T>: ServiceResult
    {
        public T Result { get; set; }
    }
}