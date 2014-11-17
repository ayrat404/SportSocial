using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using AutoMapper;
using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Common.Services.Rating;
using BLL.Infrastructure.Map;
using DAL.DomainModel;
using DAL.DomainModel.BlogEntities;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

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

        public ServiceResult CreatePost(CreatePostModel createPostModel)
        {
            Mapper.CreateMap<CreatePostModel, Post>().ForMember(o => o.Rubric, opt => opt.Ignore());
            var post = Mapper.Map<CreatePostModel, Post>(createPostModel);
            post.Status = BlogPostStatus.New;
            post.RubricId = createPostModel.Rubric;
            post.UserId = _currentUser.UserId;
            post.Lang = Thread.CurrentThread.CurrentCulture.Name;
            if (createPostModel.Images != null && _repository.Find<BlogImage>(createPostModel.Images[0].Id) != null)
            {
                post.ImageUrl = createPostModel.Images[0].Url;//TODO проверять на наличие изображения в базе
            }
            _repository.Add(post);
            _repository.SaveChanges();
            return new ServiceResult() {Success = true};
        }

        public IEnumerable<Rubric> GetRubrics()
        {
            return _repository.GetAll<Rubric>();
        }

        public void ChangeStatus(int id, int status)
        {
            var post = _repository.Find<Post>(id);
            post.Status = (BlogPostStatus) status;
            _repository.SaveChanges();
        }

        public IEnumerable<PostForAdminViewModel> GetPostsForAdmin(BlogPostStatus? status, string query)
        {
            return _repository
                .GetAll<Post>()
                .MapEachTo<PostForAdminViewModel>();
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
                .AsNoTracking()
                .MapEachTo<Comment>();
            return comments;
        }

        public BlogPostViewModel GetPost(int id)
        {
            var post =  _repository.Queryable<Post>()
                .Where(p => p.Id == id && !p.Deleted)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Single();
            var postvm = post.MapTo<BlogPostViewModel>();
            return postvm;
        }

        public PostListViewModel GetPosts(int pageSize, PostSortType sortType, int rubricId = 0, int page = 1)
        {
            int take = page * pageSize;
            int skip = take - pageSize;
            var postListVM = new PostListViewModel();
            postListVM.PageInfo = new PageInfo {CurrentPage = page};
            postListVM.PageInfo.Count = _repository
                .Queryable<Post>()
                .Count(x => x.RubricId == rubricId || rubricId == 0);
            if (sortType == PostSortType.Last)
            {
                postListVM.PostPreview = _repository
                    .Queryable<Post>()
                    .Where(x => x.RubricId == rubricId || rubricId == 0)
                    .OrderByDescending(p => p.Created)
                    .Take(take)
                    .Skip(skip)
                    .AsNoTracking()
                    .MapEachTo<PostPreviewViewModel>()
                    .ToList();
                return postListVM;
            }
            postListVM.PostPreview = _repository
                .Queryable<Post>()
                .Where(x => x.RubricId == rubricId || rubricId == 0)
                .OrderByDescending(p => p.TotalRating)
                .Take(take)
                .Skip(skip)
                .AsNoTracking()
                .MapEachTo<PostPreviewViewModel>()
                .ToList();
                return postListVM;
        }

        public EditPostModel GetEditModel(int id)
        {
            var post =  _repository
                .Find<Post>(id)
                .MapTo<EditPostModel>();
            post.Rubrics = GetRubrics();
            return post;
        }

        public ServiceResult EditPost(EditPostModel model)
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
            }
            model.MapTo(post);
            _repository.SaveChanges();
            return result;
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