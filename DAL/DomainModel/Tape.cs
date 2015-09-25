using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;
using DAL.DomainModel.JournalEntities;

namespace DAL.DomainModel
{
    [Table("Tape")]
    public class Tape: IEntity, IAuditable
    {
        public int Id { get; set; }
        public int? JournalId { get; set; }
        public int? AchievemetId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
 
        public AppUser User { get; set; }
        public Journal Journal { get; set; }
        public Achievement.Achievement Achievement { get; set; }
    }
}