using DAL.DomainModel;

namespace BLL.Common.Services.CurrentUser
{
    public interface ICurrentUser
    {
        AppUser User { get; }
        int UserId { get; }
        string UserName { get; }
        string Phone { get; }
        bool IsAnonimous { get; }
        bool IsPaid { get; }
        bool IsAdmin { get; }
        bool IsInRole(string role);
        int UnreadedNews { get; }
        void ReadAllNews();
    }
}