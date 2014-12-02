using System;
using System.Net.Http;
using System.Text;
using HttpMethod = BLL.Common.Objects.HttpMethod;

namespace BLL.Common.Helpers
{
    public class HttpHelper
    {
        public static void Send(string url, HttpMethod method, string stringToSend, string contentType)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(stringToSend, Encoding.UTF8, contentType);
                var response = client.PostAsync(url, httpContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось отправить смс");
                }
                string result = response.Content.ReadAsStringAsync().Result;
            }
        }

        public static string Get(string url, string query)
        {
            url = url + "?" + query;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось отправить");
                }
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }
    }
}