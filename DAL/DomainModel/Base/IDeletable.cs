namespace DAL.DomainModel
{
    public interface IDeletable
    {
        bool Deleted { get; set; }
    }
}