using System;

namespace BLL.Core.Services.Settings.Objects
{
    public class ProfileSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public SexVm Gender { get; set; }
        public SportExpirienceVm SportTime { get; set; }
    }
}