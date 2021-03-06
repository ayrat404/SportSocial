﻿using System;
using System.ComponentModel.DataAnnotations;
using DAL.DomainModel.EnumProperties;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class Pay: IEntity, IAuditable, IDeletable
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public PayType PayType { get; set; }

        public decimal Amount { get; set; }

        public int ProductCount { get; set; }

        public PaySatus PaySatus { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}