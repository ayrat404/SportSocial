using System.ComponentModel;

namespace DAL.DomainModel.EnumProperties
{
    public enum BlogPostStatus: byte
    {
        [Description("На модерации")]
        New = 0,

        [Description("Принята")]
        Allow = 1,

        [Description("Отклонено")]
        Rejected = 2,
        
        [Description("На модерации")]
        Fortress = 3
    }

    //public static class BlogPostStatusDescriptions {
    //    public const string New = "На модерации"
    //}
}