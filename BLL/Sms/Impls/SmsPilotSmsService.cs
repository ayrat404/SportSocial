using System.Collections.Generic;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Sms.Objects;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BLL.Sms.Impls
{
    public class SmsPilotSmsService: SmsServiceBase
    {
        public SmsPilotSmsService(EntityDbContext db) : base(db)
        {
        }

        public override void SendMessage(string msg, string phoneNumber)
        {
            string url = "http://smspilot.ru/api2.php";
            SmsPilotSendModel sendModel = new SmsPilotSendModel()
            {
                Send = new List<Send>(),
            };
            var send = new Send()
            {
                To = phoneNumber,
                Text = msg,
            };
            sendModel.Send.Add(send);
            string stringToSend = JsonConvert.SerializeObject(sendModel, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            HttpHelper.Send(url, HttpMethod.Post, stringToSend, "application/json");
            base.SendMessage(msg, phoneNumber);
        }
    }
}