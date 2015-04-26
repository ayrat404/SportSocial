using System.Collections.Generic;

namespace BLL.Admin.Conference.ViewModels
{
    public class ConferenceHostiryModel
    {
        public IEnumerable<ConfModel> Hostiry { get; set; }

        public ConfModel Current { get; set; }
    }
}