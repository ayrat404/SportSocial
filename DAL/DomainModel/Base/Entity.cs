namespace DAL.DomainModel
{
    public abstract class Entity: IEntity
    {
        public virtual int Id { get; set; }
    }
}