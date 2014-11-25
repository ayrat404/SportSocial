using System.Collections.Generic;
using BLL.Admin.Conference.ViewModels;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;

namespace BLL.Admin.Conference
{
    public interface IConferenceService
    {
        IEnumerable<ConfModel> GetAll();
        ServiceResult Create(CreateConfModel model);
        ServiceResult Edit(ConfModel model);
        ServiceResult ChangeStatus(int id, ConfStatus status);
        ConfModel GetConf(int id);
        ProcessConfModel GetInProcessConf(int id);
        ConfModel GetLastConf();
    }
}