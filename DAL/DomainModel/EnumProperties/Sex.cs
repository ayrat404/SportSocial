using System.ComponentModel;
using Knoema.Localization;

namespace DAL.DomainModel.EnumProperties
{
    public enum Sex
    {
        [Description("Муж")]
        Male = 1,
        [Description("Жен")]
        Female = 2
    }
}