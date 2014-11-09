using System.Collections.Generic;
using DAL.DomainModel.BlogEntities;

namespace DAL.DomainModel.Interfaces
{
    public interface IHasComments<THasCommentEntity, TCommentEntity>
    {
        ICollection<ICommentEntity<THasCommentEntity, TCommentEntity>> Comments { get; set; }
    }
}