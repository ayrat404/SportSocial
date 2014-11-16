using System.Collections.Generic;
using BLL.Admin.Conference.ViewModels;
using DAL.DomainModel.EnumProperties;

namespace BLL.Admin.Conference
{
    public interface IConferenceService
    {
        IEnumerable<ConfModel> GetAll();
        void Create(CreateConfModel model);
        void Edit(ConfModel model);
        void ChangeStatus(int id, ConfStatus status);
        ConfModel GetConf(int id);
        ConfModel GetLastConf();
    }
}