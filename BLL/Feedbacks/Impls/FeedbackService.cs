using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using BLL.Common.Objects;
using BLL.Feedbacks.Objects;
using BLL.Infrastructure.Map;
using DAL.DomainModel.FeedBackEntities;
using DAL.Repository.Interfaces;

namespace BLL.Feedbacks.Impls
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository _repository;

        public FeedbackService(IRepository repository)
        {
            _repository = repository;
        }

        public void AddFeedback(CreateFeedbackModel createModel)
        {
            var fb = createModel.MapTo<Feedback>();
            _repository.Add(fb);
            _repository.SaveChanges();
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
                    fbsQuery = fbsQuery.OrderBy(f => f.FeedbackType);
                    break;
                case FeedbackSortType.Comments:
                    fbsQuery = fbsQuery.OrderBy(f => f.Comments.Count);
                    break;
            }
            fbsList.FeedbackPreview = fbsQuery
                .Take(take)
                .Skip(skip)
                .ToList()
                .MapEachTo<FeedbackPreviewModel>();

            return fbsList;
        }
    }
}