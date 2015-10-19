using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using BLL.Login.Impls;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Blog.Impls
{
    public class BlogService : IBlogService
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public BlogService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public ServiceResult CreatePost(PostCreateModel createPostModel)
        {
            var result = new ServiceResult { Success = true };
            var post = createPostModel.MapTo<Post>();
            var publish = _currentUser.IsAdmin || _currentUser.IsInRole("Moderator");
            post.Status = publish ? BlogPostStatus.Allow : BlogPostStatus.New;
            post.IsFortress = publish;
            post.UserId = _currentUser.UserId;
            if (createPostModel.Images != null && _repository.Find<BlogImage>(createPostModel.Images[0].Id) != null)
            {
                post.ImageUrl = createPostModel.Images[0].Url;
            }
            if (!string.IsNullOrEmpty(createPostModel.VideoUrl))
            {
                if (!YoutubeUrlHelper.UrlIsValid(createPostModel.VideoUrl))
                {
                    result.Success = false;
                    result.ErrorMessage = "Неверный формат ссылки на видео".Resource(this);
                    return result;
                }
                post.VideoUrl = YoutubeUrlHelper.EmbeddedYoutubeUrl(createPostModel.VideoUrl);
            }
            _repository.Add(post);
            _repository.SaveChanges();
            if (post.IsFortressNews)
            {
                ApplicationStateHelper.NewsCount++;
            }
            return result;
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return _repository.Queryable<Rubric>().ToList();
        }

        public async Task<IEnumerable<Rubric>> GetRubricsAsync()
        {
            return await _repository.Queryable<Rubric>().ToListAsync();
        }

        public ServiceResult ChangeStatus(int id, int status)
        {
            var post = _repository.Find<Post>(id);
            post.Status = (BlogPostStatus) status;
            _repository.SaveChanges();
            return new ServiceResult {Success = true};
        }

        public IEnumerable<PostForAdminModel> GetPostsForAdmin(BlogPostStatus? status, string query)
        {
            return _repository
                .GetAll<Post>()
                .OrderByDescending(p => p.Created)
                .MapEachTo<PostForAdminModel>();
        }

        //public ServiceResult RaitBlog(BlogRatingViewModel model) {
        //    //return _ratingService.Rate<Post, PostRating>(model.Id, model.ActionType);
        //}

        public Comment AddComment(CreateCommentViewModel createCommentViewModelModel)
        {
            var blogComment = createCommentViewModelModel.MapTo<BlogComment>();
            _repository.Add(blogComment);
            _repository.SaveChanges();
            return blogComment.MapTo<Comment>();
        }

        public IEnumerable<Comment> LoadComments(int postId)
        {
            var comments =  _repository
                .Queryable<BlogComment>()
                .Where(c => c.CommentedEntityId == postId && !c.Deleted)
                .Include(c => c.CommentFor)
                .Include(c => c.User)
                .AsNoTracking()
                .MapEachTo<Comment>();
            return comments;
        }

        public PostDisplayModel GetPost(int id)
        {
            var post =  _repository.Queryable<Post>()
                .Where(p => p.Id == id && !p.Deleted)
                .Include(p => p.User)
                .Include(p => p.Comments)
                //.Include(p => p.Comments.Select(c => c.CommentFor))
                //.Include(p => p.Comments.Select(c => c.User))
                .Include(p => p.RatingEntites)
                .Include(p => p.Rubric)
                .Single();
            var postVm = post.MapTo<PostDisplayModel>();
            postVm.IsLiked =post.RatingEntites.Any(r => r.UserId == _currentUser.UserId && r.RatingType == RatingType.Like);
            postVm.IsDisiked =post.RatingEntites.Any(r => r.UserId == _currentUser.UserId && r.RatingType == RatingType.Dislike);
            if (post.IsFortressNews)
            {
                postVm.RubricTitle = "Новости Fortress";
                postVm.AuthorName = "Fortress";
                postVm.AuthorAvatar = LoginService.DefaultFortressAvatar;
            }
            return postVm;
        }

        public async Task<PostListModel> GetPosts(int pageSize, PostSortType sortType, int rubricId = 0, int page = 1)
        {
            int take = page * pageSize;
            int skip = take - pageSize;
            var postListVM = new PostListModel();
            postListVM.PageInfo = new PageInfo {CurrentPage = page};

            IQueryable<Post> posts = _repository
                .Queryable<Post>()
                .Include(p => p.RatingEntites)
                .Include(p => p.Comments)
                .Include(p => p.User);
            IOrderedQueryable<Post> postQuery;
            int postCount;
            Expression<Func<Post, bool>> query;
            
            switch (sortType)
            {
                case PostSortType.Best:
                    query = x => (x.RubricId == rubricId || rubricId == 0)
                                 && (x.Status == BlogPostStatus.Allow || x.Status == BlogPostStatus.OnMain)
                                 && !x.IsFortressNews;
                    postCount = posts.Count(query);
                    postQuery = posts.Where(query).OrderByDescending(p => p.TotalRating).ThenBy(p => p.Id);
                    break;
                case PostSortType.Fortress:
                    query = x => (x.RubricId == rubricId || rubricId == 0)
                                    && x.Status != BlogPostStatus.Rejected
                                    && x.IsFortress 
                                    && !x.IsFortressNews;
                    postCount = posts.Count(query);
                    postQuery = posts.Where(query).OrderBy(p => p.Created);
                    break;
                default:
                    query = x => (x.RubricId == rubricId || rubricId == 0)
                                 && (x.Status == BlogPostStatus.Allow || x.Status == BlogPostStatus.OnMain)
                                 && !x.IsFortressNews;
                    postCount = posts.Count(query);
                    postQuery = posts.Where(query).OrderByDescending(p => p.Created);
                    break;
            }
            postListVM.PageInfo.Count = postCount;
            var resultPosts = await postQuery
                .Take(take)
                .Skip(skip)
                .AsNoTracking()
                .ToListAsync();
            postListVM.PostPreview = resultPosts.MapEachTo<PostPreviewModel>();
            return postListVM;
        }

        public IEnumerable<PostPreviewModel> OnMainPosts()
        {
            var postPreview = _repository
                .Queryable<Post>()
                .Where(x => x.Status == BlogPostStatus.OnMain)
                .Include(p => p.RatingEntites)
                .OrderByDescending(p => p.Created)
                .Take(3)
                .AsNoTracking()
                .MapEachTo<PostPreviewModel>()
                .ToList();
            return postPreview;
        }

        public PostEditModel GetEditModel(int id)
        {
            var post = _repository
                .Queryable<Post>()
                .Where(p => p.Id == id && p.UserId == _currentUser.UserId)
                .SingleOrDefault();
            if (post == null || (!_currentUser.IsAdmin && post.UserId != _currentUser.UserId))
                return null;
            var postVm = post.MapTo<PostEditModel>();
            postVm.Rubrics = GetRubrics();
            return postVm;
        }

        public ServiceResult EditPost(PostEditModel model)
        {
            var result = new ServiceResult
            {
                Success = true,
            };
            var post = _repository
                .Find<Post>(model.Id);
            if (post == null)
            {
                result.Success = false;
                result.ErrorMessage = "Not Found";
                return result;
            }
            post.Title = model.Title;
            if (model.Images != null && model.Images.Any())
                post.ImageUrl = model.Images[0].Url;
            if (!string.IsNullOrEmpty(model.VideoUrl))
            {
                if (!YoutubeUrlHelper.UrlIsValid(model.VideoUrl))
                {
                    result.Success = false;
                    result.ErrorMessage = "Неверный формат ссылки на видео".Resource(this);
                    return result;
                }
                post.VideoUrl = YoutubeUrlHelper.EmbeddedYoutubeUrl(model.VideoUrl);
            }
            else
            {
                post.VideoUrl = null;
            }
            post.Text = model.Text;
            post.RubricId = model.Rubric;
            var publish = _currentUser.IsAdmin || _currentUser.IsInRole("Moderator");
            post.Status = publish ? BlogPostStatus.Allow : BlogPostStatus.New;
            _repository.SaveChanges();
            return result;
        }

        public PostListModel MyPosts(int pageSize, int page = 1)
        {
            int take = page * pageSize;
            int skip = take - pageSize;
            var postListVM = new PostListModel();
            postListVM.PageInfo = new PageInfo {CurrentPage = page};
            postListVM.PageInfo.Count = _repository
                .Queryable<Post>()
                .Count(x => x.UserId == _currentUser.UserId);
            postListVM.PostPreview = _repository
                .Queryable<Post>()
                .Where(x => x.UserId == _currentUser.UserId)
                .Include(p => p.RatingEntites)
                .OrderByDescending(p => p.Created)
                .Take(take)
                .Skip(skip)
                .AsNoTracking()
                .MapEachTo<PostPreviewModel>()
                .ToList();
            return postListVM;
        }

        public PostListModel GetNews(int pageSize, int page = 1)
        {
            int take = page * pageSize;
            int skip = take - pageSize;
            var postListVm = new PostListModel();
            postListVm.PageInfo = new PageInfo {CurrentPage = page};
            postListVm.PageInfo.Count = _repository
                .Queryable<Post>()
                .Count(x => x.IsFortressNews);
            postListVm.PostPreview = _repository
                .Queryable<Post>()
                .Where(x => x.IsFortressNews)
                .Include(p => p.RatingEntites)
                .OrderByDescending(p => p.Created)
                .Take(take)
                .Skip(skip)
                .AsNoTracking()
                .MapEachTo<PostPreviewModel>()
                .ToList();
            _currentUser.ReadAllNews();
            return postListVm;
        }

        //public PostListViewModel GetPosts(int page, PostSortType sortType = PostSortType.Last, int rubricId = 0)
        //{
        //    int viewedPosts = 10;
        //    int take = page*10;
        //    //int skip = 10
        //    if (sortType == PostSortType.Last)
        //    {
        //        var posts = _repository
        //            .Queryable<Post>()
        //            .Where(x => x.RubricId == rubricId && rubricId > 0)
        //            .OrderByDescending(p => p.Created)
        //            .Take(page )
        //            .AsNoTracking()
        //            .ToList();
        //    }
        //}
    }
}