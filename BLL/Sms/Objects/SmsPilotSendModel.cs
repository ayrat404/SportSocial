using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BLL.Sms.Objects
{
    public class SmsPilotSendModel
    {
        public SmsPilotSendModel()
        {
            Apikey = "WOP0Q143285520D2IA7R4M5ZP72VXC0B13EH8461232B025NK5816CYO171O9H0E";
        }

        public string Apikey { get; set; }

        public List<Send> Send { get; set; }
    }

    public class Send
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }

        public string To { get; set; }

        public string Text { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Send_datetime { get; set; }
    }
}