namespace BLL.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveHtml(this string str)
        {
            string resultStr = str
                .Replace("<p>", "").Replace("</p>", "")
                .Replace("<b>", "").Replace("</b>", "")
                .Replace("<h1>", "").Replace("</h1>", "")
                .Replace("<h2>", "").Replace("</h2>", "")
                .Replace("<h3>", "").Replace("</h3>", "")
                .Replace("<h4>", "").Replace("</h4>", "")
                .Replace("<h5>", "").Replace("</h5>", "")
                .Replace("<h6>", "").Replace("</h6>", "")
                .Replace("</br>", "");
            return resultStr;
        }
    }
}