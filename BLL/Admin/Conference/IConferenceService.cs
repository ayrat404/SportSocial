using BLL.Admin.Conference.ViewModels;

namespace BLL.Admin.Conference
{
    public interface IConferenceService
    {
        ConferencesList GetAll();
    }
}