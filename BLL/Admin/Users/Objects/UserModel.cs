using System;
using DAL.DomainModel.EnumProperties;

namespace BLL.Admin.Users.Objects
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegDate { get; set; }
        public int Subscribes { get; set; }
        public UserStatus Status { get; set; }
    }
}