using BLL.Common.Helpers;

namespace BLL.Common.Services.Cookies
{
    public interface ICookiesService
    {
        int GetReadedNews();
        void SetReadedNews(int count);
        bool ExistReadedNews();
    }

    public class CookiesService : ICookiesService
    {
        private const string ReadedNewsCookieName = "fortress_readedNews";

        public int GetReadedNews()
        {
            string readedNewsCookie = CookieHelper.GetValue(ReadedNewsCookieName);
            int readNews;
            if (int.TryParse(readedNewsCookie, out readNews))
            {
                return readNews;
            }
            return 0;
        }

        public void SetReadedNews(int count)
        {
            CookieHelper.SetValue(ReadedNewsCookieName, count.ToString());
        }

        public bool ExistReadedNews()
        {
            return !string.IsNullOrEmpty(CookieHelper.GetValue(ReadedNewsCookieName));
        }
    }
}