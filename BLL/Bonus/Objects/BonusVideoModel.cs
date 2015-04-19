namespace BLL.Bonus.Objects
{
    public class BonusVideoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
    }
}