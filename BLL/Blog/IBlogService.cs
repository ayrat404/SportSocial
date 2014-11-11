using System.Collections.Generic;
using System.Web.UI;
using BLL.Blog.ViewModels;
using BLL.Comments.Objects;
using BLL.Common.Objects;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;

namespace BLL.Blog
{
    public interface IBlogService
    {
        ServiceResult CreatePost(CreatePostModel createPostModel);
        IEnumerable<Rubric> GetRubrics();
        void ChangeStatus(int id, int status);
        IEnumerable<PostForAdminViewModel> GetPostsForAdmin(BlogPostStatus? status, string query);
        //ServiceResult RaitBlog(BlogRatingViewModel model);
        Comment AddComment(CreateCommentViewModel createCommentViewModelModel);
        IEnumerable<Comment> LoadComments(int postId);
        BlogPostViewModel GetPost(int id);
        PostListViewModel GetPosts(PostSortType sortType, int rubricId = 0, int page = 1);
    }

    public enum PostSortType
    {
        Last,
        Best,
    }
}