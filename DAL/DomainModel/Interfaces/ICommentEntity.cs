using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.DomainModel.FeedBackEntities;

namespace DAL.DomainModel.Interfaces
{
    public abstract class CommentEntityBase: IEntity, IAuditable, IDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool ByFortress { get; set; }
        public int UserId { get; set; }
        public int? CommentForId { get; set; }
        public int CommentedEntityId { get; set; }
        public DateTime Created  { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }

    public abstract class CommentEntity<TCommentFor> : CommentEntityBase
        where TCommentFor: CommentEntityBase
    {

        [ForeignKey("CommentForId")]
        public TCommentFor CommentFor { get; set; }
    }

    public abstract class CommentEntity<TCommentFor, TCommentedEntity> : CommentEntityBase
        where TCommentFor: CommentEntityBase
        where TCommentedEntity: class
    {
        [ForeignKey("CommentForId")]
        public TCommentFor CommentFor { get; set; }

        [ForeignKey("CommentedEntityId")]
        public virtual TCommentedEntity CommentedEntity { get; set; }
    }
}