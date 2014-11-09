using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BLL.Common.Extensions
{
    static public class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
    }
}