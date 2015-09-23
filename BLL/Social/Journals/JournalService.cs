using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Social.Tags;
using BLL.Storage;
using BLL.Storage.Impls.Enums;
using BLL.Storage.Objects;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Social.Journals
{
    public class JournalService : IJournalService
    {
        private readonly ITagService _tagService;
        private readonly IJournalRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IImageService _imageService;
        private readonly IVideoService _videoService;

        public JournalService(ITagService tagService, IJournalRepository repository, ICurrentUser currentUser, IImageService imageService, IVideoService videoService)
        {
            _tagService = tagService;
            _repository = repository;
            _currentUser = currentUser;
            _imageService = imageService;
            _videoService = videoService;
        }

        public bool CanAction(Journal journal)
        {
            return journal.UserId == _currentUser.UserId;
        }

        public ServiceResult<JournalDisplayVm> CreateJournal(JournalVm journalModel)
        {
            var journal = new Journal
            {
                Text = journalModel.Text,
                UserId = _currentUser.UserId
            };
            _repository.Add(journal);
            _repository.SaveChanges();
            _tagService.AddTags(journal.Id, journalModel.Tags);
            _imageService.AttachImagesToEntity(journalModel.Media.Where(m => m.Type == MediaType.Image).ToList(), journal.Id, UploadType.Journal);
            _videoService.AttachVideosToEntity(journalModel.Media.Where(m => m.Type == MediaType.Video).ToList(), journal.Id, UploadType.Journal);
            return ServiceResult.SuccessResult(journal.MapTo<JournalDisplayVm>());
        }

        public ServiceResult EditJournal(JournalVm journalModel)
        {
            var journal = _repository.JournalForEdit(journalModel.Id);
            if (!CanAction(journal))
            {
                return ServiceResult.ErrorResult("");
            }
            journal.Text = journalModel.Text;
            _repository.DeleteRange(journal.Media);
            _repository.DeleteRange(journal.Tags);
            _repository.SaveChanges();
            _tagService.AddTags(journal.Id, journalModel.Tags);
            _imageService.AttachImagesToEntity(journalModel.Media.Where(m => m.Type == MediaType.Image).ToList(), journal.Id, UploadType.Journal);
            _videoService.AttachVideosToEntity(journalModel.Media.Where(m => m.Type == MediaType.Video).ToList(), journal.Id, UploadType.Journal);
            return ServiceResult.SuccessResult();
        }

        public ServiceResult DeleteJournal(int id)
        {
            var journal = _repository.Find<Journal>(id);
            if (!CanAction(journal))
            {
                return ServiceResult.ErrorResult("");
            }
            _repository.Delete(journal);
            _repository.SaveChanges();
            return ServiceResult.SuccessResult();
        }

        public PagedListVm<JournalPreviewVm> GetJournals(int userId, int page, int count)
        {
            int skip = count*page - count;
            var journals = _repository.GetJournals(userId, skip, count);
            return new PagedListVm<JournalPreviewVm>
            {
                IsMore = count*page < journals.Count,
                List = journals.List.MapEachTo<JournalPreviewVm>()
            };
        }

        public JournalDisplayVm GetJournal(int journalId)
        {
            var journal = _repository.GetJournal(journalId);
            return journal.MapTo<JournalDisplayVm>();
        }
    }
}