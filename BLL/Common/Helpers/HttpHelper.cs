using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpMethod = BLL.Common.Objects.HttpMethod;

namespace BLL.Common.Helpers
{
    public class HttpHelper
    {
        public static async void Send(string url, HttpMethod method, string stringToPost, string contentType)
        {
            using (var client = new HttpClient())
            {
                var httpContent = new StringContent(stringToPost, Encoding.UTF8, contentType);
                var response = await client.PostAsync(url, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось отправить смс");
                }
                string result = await response.Content.ReadAsStringAsync();
            }
        }
    }
}