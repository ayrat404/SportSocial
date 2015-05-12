using System.ComponentModel.DataAnnotations;

namespace BLL.Feedbacks.Objects
{
    public class CreateFeedbackModel
    {
        [Required]
        public int Type { get; set; }

        [Required]
        public string Text { get; set; }
    }
}