using System.Collections.Generic;
using BLL.Blog.ViewModels;
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
        ServiceResult RaitBlog(BlogRatingViewModel model);
        void AddComment(CreateCommentViewModel createCommentViewModelModel);
        IEnumerable<Comment> LoadComments(int postId);
        BlogPostViewModel GetPost(int id);
    }
}