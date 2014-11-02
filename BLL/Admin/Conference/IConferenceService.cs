using System.Collections.Generic;
using BLL.Admin.Conference.ViewModels;

namespace BLL.Admin.Conference
{
    public interface IConferenceService
    {
        IEnumerable<ConfModel> GetAll();
        void Create(CreateConfModel model);
        void Edit(ConfModel model);
    }
}