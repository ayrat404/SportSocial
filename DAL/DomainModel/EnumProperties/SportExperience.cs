using System.ComponentModel;

namespace DAL.DomainModel.EnumProperties
{
    public enum SportExperience
    {
        [Description("Меньше года")]
        LessOne,
        [Description("От 1 до 2")]
        One,
        [Description("От 2 до 3")]
        Two,
        [Description("От 3 до 4")]
        Three,
        [Description("От 4 до 5")]
        Four,
        [Description("От 5 до 7")]
        FiveSeven,
        [Description("От 7 до 10")]
        SevenTen,
        [Description("Больше 10")]
        MoreTen
    }
}