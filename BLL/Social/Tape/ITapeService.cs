using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.MapProfiles;
using BLL.Social.Journals.Objects;
using BLL.Social.UserProfile;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace BLL.Social.Tape
{
    public interface ITapeService
    {
        PagedListVm<TapeVm> GetTape(int page, int count);
        void AddToTape(int entityId, TapeType tapeType);
    }

    public class TapeService : ITapeService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRepository _repository;

        public TapeService(ICurrentUser currentUser, IRepository repository)
        {
            _currentUser = currentUser;
            _repository = repository;
        }

        public void AddToTape(int entityId, TapeType tapeType)
        {
            var tape = new DAL.DomainModel.Tape()
            {
                UserId = _currentUser.UserId,
            };
            if (tapeType == TapeType.Achievement)
                tape.AchievemetId = entityId;
            else
                tape.JournalId = entityId;
            _repository.Add(tape);
            //_repository.SaveChanges();
        }

        public PagedListVm<TapeVm> GetTape(int page, int count)
        {
            int skip = count*page - count;
            var usersId = _repository.Queryable<Subscribe>()
                .Where(s => s.FolowerUserId == _currentUser.UserId)
                .Select(s => s.ToUserId)
                .ToArray();

            int totalCount = _repository.Queryable<DAL.DomainModel.Tape>()
                .Count(t => usersId.Contains(t.UserId));
            var result = _repository.Queryable<DAL.DomainModel.Tape>()
                .Where(t => usersId.Contains(t.UserId)
                         && (t.JournalId !=null || (t.AchievemetId != null && t.Achievement.Started != null)))
                .Include(t => t.Journal.RatingEntites)
                .Include(t => t.Journal.RatingEntites.Select(r => r.User))
                .Include(t => t.Journal.User)
                .Include(t => t.Journal.Media)
                .Include(t => t.Journal.Tags)
                .Include(t => t.Journal.Tags.Select(jt => jt.Tag))
                .Include(t => t.Journal.Media.Select(m => m.RatingEntites))
                .Include(t => t.Achievement.Voices)
                .Include(t => t.Achievement.User)
                .Include(t => t.Achievement.AchievementType)
                .Include(t => t.Achievement.Value)
                .OrderByDescending(t => t.Id)
                .Skip(skip)
                .Take(count)
                .ToList();
            var tapeList = new List<TapeVm>();
            foreach (var tape in result)
            {
                var tapVm = new TapeVm();
                if (tape.AchievemetId != null)
                {
                    tapVm.Type = TapeType.Achievement;
                    tapVm.Object = tape.Achievement.MapTo<AchievementPreviewVm>();
                }
                else
                {
                    tapVm.Type = TapeType.Record;
                    tapVm.Object = tape.Journal.MapTo<JournalPreviewVm>();
                }
                tapeList.Add(tapVm);
            }
            var pagedList = new PagedListVm<TapeVm>()
            {
                List = tapeList,
                IsMore = count*page < totalCount
            };
            return pagedList;
        }
    }
}