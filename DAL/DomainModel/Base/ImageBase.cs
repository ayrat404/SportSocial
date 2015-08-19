using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Base
{
    public class ImageBase<TEnity>: IEntity, IAuditable where TEnity: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Url { get; set; }

        [Column("EntityId")]
        [ForeignKey("Enity")]
        public int? EntityId { get; set; }

        public TEnity Enity { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified{ get; set; }
    }
}