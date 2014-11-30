using System.Globalization;

namespace BLL.Common.Extensions
{
    public static class DecimalExtension
    {
        public static string ToStringWithDot(this decimal obj)
        {
            return obj.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}