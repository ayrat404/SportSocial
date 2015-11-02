using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using DAL.DomainModel.Achievement.Objects;
using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel.Achievement
{
    public class AchievementVoice : IEntity
    {
        public int Id { get; set; }
        public bool VoteFor { get; set; }
        public int AchievementId { get; set; }
        public int UserId { get; set; }

        public AppUser User { get; set; }
        public Achievement Achievement { get; set; }
    }

    public class Achievement: IEntity, IAuditable, IDeletable, IHasComments<AchievementComment>, IHasRating<AchievementRating>
    {
        public Achievement()
        {
            AchievementMedia = new List<AchievementMedia>();
            RatingEntites = new List<AchievementRating>();
            Comments = new List<AchievementComment>();
            Voices = new List<AchievementVoice>();
        }

        public int Id { get; set; }

        public int Step { get; set; }

        //public string Value { get; set; }

        public AchievementStatus Status { get; set; }

        public int TypeId { get; set; }

        public int UserId { get; set; }

        public int DurationDays { get; set; }

        public int TotalRating { get; set; }
        
        public double VotesRatio { get; set; }

        public int ValueId { get; set; }

        public DateTime? Started { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public bool Deleted { get; set; }


        //public int GetRemainingTime()
        //{
        //    DateTime.Now - new DateTime()
        //}

        public AchievementStatus GetStatus()
        {
            if (Status == AchievementStatus.InCreating || !Started.HasValue)
                return Status;
            if (GetTimeStamp() > 0)
                return AchievementStatus.Started;
            return VotesRatio >= 0.75 ? AchievementStatus.Credit : AchievementStatus.Fail;
        }

        public double ReCalculateVoteRatio()
        {
            var voteForCount = Voices.Count(v => v.VoteFor);
            var voteAgainstCount = Voices.Count(v => !v.VoteFor);
            if (voteAgainstCount == 0)
            {
                VotesRatio = 1;
            }
            else
            {
                VotesRatio = (double) voteForCount/(voteAgainstCount + voteForCount);
            }
            return VotesRatio;
        }


        public long GetTimeStamp()
        {
            if (!Started.HasValue)
                return 0;
            var timeStamp = (long)(DurationDays*3600*24*1000 - (DateTime.Now - Started.Value).TotalMilliseconds);
            if (timeStamp < 0)
                return 0;
            return timeStamp;
        }

        public bool UserAlreadyVoted(int userId)
        {
            return Voices.Any(v => v.UserId == userId);
        }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        [ForeignKey("TypeId")]
        public AchievementType AchievementType { get; set; }
        public ICollection<AchievementMedia> AchievementMedia { get; set; }
        public ICollection<AchievementRating> RatingEntites { get; set; }
        public ICollection<AchievementComment> Comments { get; set; }
        public ICollection<AchievementVoice> Voices { get; set; }
        public AchievementTypeValue Value { get; set; }
    }
}