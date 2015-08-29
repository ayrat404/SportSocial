using System.Web;

namespace BLL.Common.Helpers
{
    public static class SessionStateHelper
    {
        public static int GetReadedNews()
        {
            var reededNews = HttpContext.Current.Session["ReadedNew"];
            if (reededNews == null)
            {
                HttpContext.Current.Session["ReadedNew"] = 0;
                return 0;
            }
            else
            {
                return (int) reededNews;
            }
        }

        public static void SetReadedNews(int count)
        {
            HttpContext.Current.Session["ReadedNew"] = count;
        }
    }
}