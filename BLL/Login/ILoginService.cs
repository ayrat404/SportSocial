using BLL.Common.Objects;
using BLL.Login.Impls;
using BLL.Login.ViewModels;
using BLL.Storage;

namespace BLL.Login
{
    public interface ILoginService
    {
        ServiceResult<SingInResult> SignIn(SignInModel signInModel, string returnUrl);

        LoginServiceResult PreRegister(RegistratioinModel regModel, string url);

        ServiceResult ConfirmSmsCode(ConfirmSmsCode regModel);

        ServiceResult ResendSmsCode(string phone);

        ServiceResult ChangePassword(ChangePaswdModel changePaswdModel);

        ServiceResult RestorePassword(string phone);

        ServiceResult RestorePasswordConfirm(RestorePasswordInfo confirmModel);

        ServiceResult ChangePhone(string phone);

        ServiceResult ChangePhoneConfirm(ChangePhoneModel chPhoneModel);

        ServiceResult LogOut();

        ServiceResult<ImageUploadResult> RemoveAvatar();
    }
}