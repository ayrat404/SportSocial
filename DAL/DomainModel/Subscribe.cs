using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class Subscribe : IEntity, IAuditable
    {
        public int Id { get; set; }

        //[ForeignKey("FolowerUser")]
        public int FolowerUserId { get; set; }

        //[ForeignKey("ToUser")]
        public int ToUserId { get; set; }

        public AppUser FolowerUser { get; set; }

        public AppUser ToUser { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}