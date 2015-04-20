using System.Collections.Generic;
using BLL.Admin.Users.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Admin.Users
{
    public interface IUserManagmentService
    {
        IEnumerable<UserModel> GetUsers();
        void ChangeUserStatus(int userId, UserStatus newStatus);
        UsersStatistic GetUsersStatistic();
    }
}