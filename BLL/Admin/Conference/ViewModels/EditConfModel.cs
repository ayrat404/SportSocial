using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using DAL.DomainModel.EnumProperties;
using Newtonsoft.Json;

namespace BLL.Admin.Conference.ViewModels
{
    public class ConfModel: CreateConfModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ConfStatus Status { get; set; }

        public int Stamp { get; set; }
    }

    public class ConferenceHostiryModel
    {
        public IEnumerable<ConfModel> Hostiry { get; set; }

        public ConfModel Current { get; set; }
    }

    public class ProcessConfModel: IHasCommentViewModel
    {
        public int Id { get; set; }

        public ConfStatus Status { get; set; }

        public int Stamp { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }
        public int MoreCommentsCount { get; set; }
        public CommentItemType ItemType { get; set; }
        public Comment[] Comments { get; set; }
    }
}