namespace BLL.Comments.Objects
{
    public class CommentsInfo
    {
        public int Id { get; set; }

        public Comment[] Comments { get; set; }

        public int MoreCommentsCount { get; set; }

        public CommentItemType ItemType { get; set; }
    }
}