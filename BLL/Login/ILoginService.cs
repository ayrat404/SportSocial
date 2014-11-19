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

        ServiceResult ChangePassword(ChangePaswdModel changePaswdModel);

        ServiceResult RestorePassword(string phone);

        ServiceResult RestorePasswordConfirm(ConfirmSmsCode confirmModel);

        ServiceResult ChangePhone(string phone);

        ServiceResult ChangePhoneConfirm(ChangePhoneModel chPhoneModel);
    }
}