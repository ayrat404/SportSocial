using System;
using System.ComponentModel.DataAnnotations;
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
}