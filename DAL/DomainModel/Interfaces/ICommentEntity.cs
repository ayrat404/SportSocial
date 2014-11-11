namespace DAL.DomainModel.Interfaces
{
    public interface ICommentEntity<TCommentedEntity>: IEntity, IAuditable 
        where TCommentedEntity: class
    {
        string Text { get; set; }
        string UserId { get; set; }
        int? CommentForId { get; set; }
        int CommentedEntityId { get; set; }
        TCommentedEntity CommentedEntity { get; set; }
        AppUser User { get; set; }
    }
}