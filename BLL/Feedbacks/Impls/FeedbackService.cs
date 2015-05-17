using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Feedbacks.Objects;
using BLL.Infrastructure.Map;
using DAL.DomainModel.FeedBackEntities;
using DAL.Repository.Interfaces;

namespace BLL.Feedbacks.Impls
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public FeedbackService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public FeedbackPreviewModel AddFeedback(CreateFeedbackModel createModel)
        {
            var fb = createModel.MapTo<Feedback>();
            _repository.Add(fb);
            _repository.SaveChanges();
            fb = _repository.Queryable<Feedback>()
                .Include(f => f.Comments)
                .Include(f => f.RatingEntites)
                .Include(f => f.User)
                .Single(f => f.Id == fb.Id);
            return fb.MapTo<FeedbackPreviewModel>();
        }

        public FeedbackListModel GetFeedbacks(int pageSize, FeedbackSortType sortType, int page = 1)
        {
            int take = page * pageSize;
            int skip = take - pageSize;
            var fbsList = new FeedbackListModel();

            fbsList.PageInfo.Count = _repository.Queryable<Feedback>().Count();
            fbsList.PageInfo.CurrentPage = page;

            IQueryable<Feedback> fbsQuery = _repository.Queryable<Feedback>()
                .Include(f => f.Comments)
                .Include(f => f.RatingEntites)
                .Include(f => f.User);
            switch (sortType)
            {
                case FeedbackSortType.Rating:
                    fbsQuery = fbsQuery.OrderByDescending(f => f.TotalRating).ThenByDescending(f => f.Id);
                    break;
                case FeedbackSortType.Comments:
                    fbsQuery = fbsQuery.OrderByDescending(f => f.Comments.Count).ThenByDescending(f => f.Id);
                    break;
                default:
                    fbsQuery = fbsQuery.OrderByDescending(f => f.Id);
                    break;
            }
            fbsList.FeedbackPreview = fbsQuery
                .Take(take)
                .Skip(skip)
                .ToList()
                .MapEachTo<FeedbackPreviewModel>();

            return fbsList;
        }

        public void Remove(int id)
        {
            var fb = _repository.Find<Feedback>(id);
            if (fb == null)
                return;
            if (_currentUser.IsAdmin || _currentUser.UserId == fb.UserId)
            {
                _repository.Delete(fb);
                _repository.SaveChanges();
            }
        }
    }
}