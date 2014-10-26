using BLL.Common.Objects;

namespace BLL.Login.ViewModels
{
    public class LoginServiceResult: ServiceResult
    {
        public string ReturnUrl { get; set; }
    }
}