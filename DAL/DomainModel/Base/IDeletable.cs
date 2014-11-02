namespace DAL.DomainModel.Base
{
    public interface IDeletable
    {
        bool Deleted { get; set; }
    }
}