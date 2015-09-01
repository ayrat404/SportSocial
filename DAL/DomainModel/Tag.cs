using System.Collections.Generic;
using DAL.DomainModel.JournalEntities;

namespace DAL.DomainModel
{
    public class Tag
    {
        public int Id { get; set; }
        public string Label { get; set; }

        public ICollection<JournalTag> JournalTags { get; set; }
    }
}