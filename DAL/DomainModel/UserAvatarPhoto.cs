﻿using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class UserAvatarPhoto: IEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}