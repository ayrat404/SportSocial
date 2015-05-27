using System.Linq;
using System.Web.Mvc;
using DAL.DomainModel.BlogEntities;
using DAL.Repository.Interfaces;

namespace BLL.Common.Helpers
{
    public static class ApplicationStateHelper
    {
        public static int NewsCount;

        static ApplicationStateHelper()
        {
            NewsCount = DependencyResolver.Current.GetService<IRepository>()
                .Queryable<Post>()
                .Count(p => p.IsFortressNews);
        }
    }
}