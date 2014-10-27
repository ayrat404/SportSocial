using System.Web;

namespace BLL.Blog.ViewModels
{
    public class PostModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public HttpPostedFileBase Image { get; set; }

        //public 
    }
}