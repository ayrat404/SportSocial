using System.Collections.Generic;
using System.Linq;
using BLL.Comments.Impls;
using BLL.Infrastructure.IdentityConfig;
using BLL.Social.Journals.Objects;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace BLL.Storage.Impls
{
    public class AttacherMediaWorker : MediaWorkerBase, IAttacherMediaWorker
    {
        private readonly IRepository _repository;

        public AttacherMediaWorker(IRepository repository)
        {
            _repository = repository;
        }

        public void AttachMediaToEntity(List<MediaVm> media, int entityId, UploadType uploadType)
        {
            GetGenericMethod("AttachMediaToEntity", uploadType).Invoke(this, new object[] { media, entityId });
        }

        private void AttachMediaToEntity<TEntity, TMedia>(List<MediaVm> attachedMedia, int entityId)
            where TEntity : class, IEntity
            where TMedia : MediaBase<TEntity>
        {
            var mediaIds = attachedMedia.Select(v => v.Id);
            var medias = _repository.Queryable<TMedia>()
                .Where(m => mediaIds.Contains(m.Id) && m.EntityId == null)
                .ToList();
            foreach (var media in medias)
            {
                media.EntityId = entityId;
            }
            _repository.SaveChanges();
        }

    }
}