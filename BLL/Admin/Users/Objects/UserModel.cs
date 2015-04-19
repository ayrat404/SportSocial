using System;

namespace BLL.Admin.Users.Objects
{
    public class UserModel
    {
        public string Name { get; set; }
        public DateTime RegDate { get; set; }
        public int Subscribes { get; set; }
        public int Status { get; set; }
    }
}