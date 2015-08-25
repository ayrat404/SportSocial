using System.Collections.Generic;

namespace Social.Models
{
    public class ApiError
    {
        public SummaryError Common { get; set; }
        public List<FieldError> Fields { get; set; }
    }
}