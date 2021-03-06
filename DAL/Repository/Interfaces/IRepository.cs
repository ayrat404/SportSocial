﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;

namespace DAL.Repository.Interfaces
{
    public interface IRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        TEntity Find<TEntity>(object id) where TEntity : class;
        TEntity Create<TEntity>() where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Delete<TEntity>(object id) where TEntity : class;
        IQueryable<TEntity> Queryable<TEntity>() where TEntity : class;
        //IQueryable Queryable(Type type);
        void SaveChanges();
    }

    public interface IProfileRepository: IRepository
    {
        ListDto<AppUser> GetUsers(int skip, int take);
        ListDto<AppUser> GetUsers(DateTime? birthFrom, DateTime? birthTo, SportExperience? sportTime, Sex? gender, string city, string country, string query, int skip, int count);
        AppUser GetUserFull(int userId);
    }

    public class ProfileRepository : Repository, IProfileRepository
    {
        public ProfileRepository(EntityDbContext context) : base(context)
        {
        }

        public AppUser GetUserFull(int userId)
        {
            return Queryable<AppUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Profile)
                .Include(u => u.Subscribes.Select(f => f.ToUser))
                .Include(u => u.Folowers.Select(f => f.FolowerUser))
                .Include(u => u.Achievements)
                .Include(u => u.Achievements.Select(a => a.Value))
                .Single();
        }

        public ListDto<AppUser> GetUsers(int skip, int take)
        {
            int usersCount = Queryable<AppUser>().Count();
            var query = Queryable<AppUser>()
                .Include(u => u.Profile)
                .Include(u => u.Achievements)
                .Include(u => u.Journals)
                .OrderByDescending(u => u.Id)
                .Skip(skip)
                .Take(take);
            return new ListDto<AppUser>
            {
                Count = usersCount,
                List = query.ToList()
            };
        }

        public ListDto<AppUser> GetUsers(DateTime? birthFrom, DateTime? birthTo, SportExperience? sportTime, Sex? gender, string city, string country, string query, int skip, int take)
        {
            IQueryable<AppUser> userQuery = Queryable<AppUser>().Where(u => u.PhoneNumberConfirmed);
            if (birthFrom.HasValue && birthTo.HasValue)
                userQuery = userQuery.Where(u => u.Profile.BirthDate >= birthFrom && u.Profile.BirthDate <= birthTo);
            if (sportTime.HasValue)
                userQuery = userQuery.Where(u => u.Profile.Experience == sportTime.Value);
            if (gender.HasValue)
                userQuery = userQuery.Where(u => u.Profile.Sex == gender.Value);
            if (!string.IsNullOrWhiteSpace(query))
                userQuery = userQuery.Where(u => u.Profile.FirstName.ToLower().Contains(query.ToLower()) 
                                              || u.Profile.LastName.ToLower().Contains(query.ToLower())
                                              || u.Name.ToLower().Contains(query.ToLower()));
            int usersCount = userQuery.Count();
            var resultQuery = userQuery
                .Include(u => u.Profile)
                .Include(u => u.Achievements)
                .Include(u => u.Journals)
                .Include(u => u.Folowers)
                .Include(u => u.Folowers.Select(f => f.FolowerUser))
                .OrderByDescending(u => u.Achievements.Count(a => a.Started != null))
                .ThenByDescending(u => u.Journals.Count())
                .ThenByDescending(u => u.Id)
                .Skip(skip)
                .Take(take);
            return new ListDto<AppUser>
            {
                Count = usersCount,
                List = resultQuery.ToList()
            };
        }


    }
}