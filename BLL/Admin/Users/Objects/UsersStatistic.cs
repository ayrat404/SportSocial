namespace BLL.Admin.Users.Objects
{
    public class UsersStatistic
    {
        public int UsersCount { get; set; }
        public int NotConfirmedUsersCount { get; set; }
        public int PayedUsers { get; set; }
        public int PayedMounths { get; set; }
        public double AverageMounths { get; set; }
    }
}