using System.Linq;
using BLL.Admin.Users.Objects;
using DAL.DomainModel;
using Profile = AutoMapper.Profile;

namespace BLL.Admin.Users.MapProfiles
{
    public class UserModelProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<AppUser, UserModel>()
                .ForMember(dest => dest.RegDate, opt => opt.MapFrom(s => s.Created))
                .ForMember(dest => dest.Subscribes, opt => opt.MapFrom(s => s.Pays.Sum(p => p.ProductId)));
        }
    }
}