using System;
using System.Collections.Generic;
using BLL.Blog.ViewModels;
using BLL.Rating.Objects;

namespace BLL.Social.Journals.Objects
{
    public class JournalPreviewVm
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public UserInfoVm UserInfo { get; set; }

        public RatingInfo Likes { get; set; }

        public List<MediaVm> Media { get; set; }
       
        public DateTime Created { get; set; }

        public List<string> Tags { get; set; }
    }
}