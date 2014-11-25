using System;
using System.Net.Http;
using System.Text;
using HttpMethod = BLL.Common.Objects.HttpMethod;

namespace BLL.Common.Helpers
{
    public class HttpHelper
    {
        public static void Send(string url, HttpMethod method, string stringToPost, string contentType)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(stringToPost, Encoding.UTF8, contentType);
                var response = client.PostAsync(url, httpContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось отправить смс");
                }
                string result = response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}