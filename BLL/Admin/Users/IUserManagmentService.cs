using System.Collections.Generic;
using BLL.Admin.Users.Objects;

namespace BLL.Admin.Users
{
    public interface IUserManagmentService
    {
        List<UserModel> GetUsers();
        void ChangeUserStatus(int userId);
        UsersStatistic GetUsersStatistic(int userId);
    }
}