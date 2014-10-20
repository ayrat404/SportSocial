using System;
using System.Net.Http;
using System.Web;

namespace BLL.Common.Helpers
{
    class CookieHelper
    {
        public static string GetValue(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
                return HttpContext.Current.Request.Cookies[key].Value;
            return string.Empty;
        }

        public static void SetValue(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
