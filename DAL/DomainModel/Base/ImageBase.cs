using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Base
{
    public class MediaBase<TEnity>: IEntity, IAuditable where TEnity: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Url { get; set; }

        //public string VideoImageUrl { get; set; }

        public int Type { get; set; }

        public int? VideoProvider { get; set; }

        [Column("EntityId")]
        [ForeignKey("Enity")]
        public int? EntityId { get; set; }

        public TEnity Enity { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified{ get; set; }
    }
}