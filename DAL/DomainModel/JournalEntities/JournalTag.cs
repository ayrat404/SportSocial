using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class JournalTag: IEntity
    {
        public int Id { get; set; }
        public int JournalId { get; set; }
        public int TagId { get; set; }

        public Journal Journal { get; set; }
        public Tag Tag { get; set; }
    }
}