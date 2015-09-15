using System.Collections.Generic;
using DAL.DomainModel.Base;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.JournalEntities
{
    public class JournalMedia: MediaBase<Journal>, IHasRating<JournalMediaRating>
    {
        public JournalMedia()
        {
            RatingEntites = new List<JournalMediaRating>();
        }

        public int TotalRating { get; set; }

        public ICollection<JournalMediaRating> RatingEntites { get; set; }
    }

    public class JournalMediaRating: RatingEntity<JournalMedia>
    {
    }
}