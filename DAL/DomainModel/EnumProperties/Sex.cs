using System.ComponentModel;
using Knoema.Localization;

namespace DAL.DomainModel.EnumProperties
{
    public enum Sex
    {
        [Description("ћуж")]
        Male = 1,
        [Description("∆ен")]
        Female = 2
    }
}