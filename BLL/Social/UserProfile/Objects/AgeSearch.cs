using System.ComponentModel;

namespace BLL.Social.UserProfile.Objects
{
    public enum AgeSearch
    {
        [Description("до 18")]
        One,
        [Description("от 18 до 25")]
        Two,
        [Description("от 25 до 40")]
        Three,
        [Description("старше 40")]
        Four
    }
}