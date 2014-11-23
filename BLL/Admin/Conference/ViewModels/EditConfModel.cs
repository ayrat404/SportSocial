using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Comments.Objects;
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

    public class ProcessConfModel: ConfModel
    {
        public IEnumerable<Comment> Comments { get; set; }
    }
}