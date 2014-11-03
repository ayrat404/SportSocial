using AutoMapper;
using BLL.Admin.Conference.ViewModels;

namespace BLL.Admin.Conference.MapProfiles
{
    public class ConferenceProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<DAL.DomainModel.Conference, ConfModel>();
            CreateMap<DAL.DomainModel.Conference, CreateConfModel>();
            CreateMap<CreateConfModel, DAL.DomainModel.Conference>();
        }
    }
}