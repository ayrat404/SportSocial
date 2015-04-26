using BLL.Blog.ViewModels;

namespace BLL.Common.Objects
{
    public interface IHasItemInfo: IHasDate
    {
        ItemInfo ItemInfo { get; set; }
    }
}