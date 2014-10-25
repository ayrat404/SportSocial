using BLL.Common.Objects;
using BLL.Login.ViewModels;

namespace BLL.Login
{
    public interface ILoginService
    {
        LoginServiceResult SignIn(SignInModel signInModel);

        LoginServiceResult PreRegister(RegistratioinModel regModel, string url);

        ServiceResult ConfirmSmsCode(ConfirmSmsCode regModel);

        ServiceResult ResendSmsCode();
    }
}