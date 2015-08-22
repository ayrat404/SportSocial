using BLL.Common.Objects;
using BLL.Login.ViewModels;
using BLL.Storage;

namespace BLL.Login
{
    public interface ILoginService
    {
        LoginServiceResult SignIn(SignInModel signInModel, string returnUrl);

        LoginServiceResult PreRegister(RegistratioinModel regModel, string url);

        ServiceResult ConfirmSmsCode(ConfirmSmsCode regModel);

        ServiceResult ResendSmsCode(string phone);

        ServiceResult ChangePassword(ChangePaswdModel changePaswdModel);

        ServiceResult RestorePassword(string phone);

        ServiceResult RestorePasswordConfirm(ConfirmSmsCode confirmModel);

        ServiceResult ChangePhone(string phone);

        ServiceResult ChangePhoneConfirm(ChangePhoneModel chPhoneModel);

        ServiceResult LogOut();

        ServiceResult<ImageUploadResult> RemoveAvatar();
    }
}