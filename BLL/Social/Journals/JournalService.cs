using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;
using BLL.Social.Tags;
using BLL.Social.Tape;
using BLL.Social.UserProfile;
using BLL.Storage;
using BLL.Storage.Impls;
using BLL.Storage.Objects;
using BLL.Storage.Objects.Enums;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Social.Journals
{
    public class JournalService : IJournalService
    {
        private readonly ITagService _tagService;
        private readonly IJournalRepository _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IMediaService _mediaService;
        private readonly ITapeService _tapeService;

        public JournalService(ITagService tagService, IJournalRepository repository, ICurrentUser currentUser, ITapeService tapeService, IMediaService mediaService)
        {
            _tagService = tagService;
            _repository = repository;
            _currentUser = currentUser;
            _tapeService = tapeService;
            _mediaService = mediaService;
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
            _tapeService.AddToTape(journal.Id, TapeType.Record);
            _tagService.AddTags(journal.Id, journalModel.Tags);
            _mediaService.AttachMediaToEntity(journalModel.Media, journal.Id, UploadType.Journal);
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
            var mediaToDelete = journal.Media.Where(m => journalModel.Media.All(u => u.Id != m.Id));
            var tagsToDelete = journal.Tags.Where(t => journalModel.Tags.All(u => u != t.Tag.Label));
            _repository.DeleteRange(mediaToDelete);
            _repository.DeleteRange(tagsToDelete);
            _repository.SaveChanges();
            _tagService.AddTags(journal.Id, journalModel.Tags);
            _mediaService.AttachMediaToEntity(journalModel.Media, journal.Id, UploadType.Journal);
            return ServiceResult.SuccessResult();
        }

        public ServiceResult DeleteJournal(int id)
        {
            var journal = _repository.Find<Journal>(id);
            if (!CanAction(journal))
            {
                return ServiceResult.ErrorResult("");
            }
            var tape = _repository.Queryable<DAL.DomainModel.Tape>()
                .Where(t => t.JournalId == id)
                .ToList();
            _repository.DeleteRange(tape);
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