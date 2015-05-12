using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BLL.Admin.Users.Objects;
using BLL.Infrastructure.Map;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Admin.Users.Impls
{
    public class UserManagmentService : IUserManagmentService
    {
        private readonly IRepository _repository;

        public UserManagmentService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = _repository.Queryable<AppUser>()
                .Where(u => u.PhoneNumberConfirmed)
                .Include(u => u.Profile)
                .Include(u => u.Pays)
                .Select(u => new UserModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    RegDate = u.Created,
                    Status = u.Status,
                    Subscribes = (int?)u.Pays.Where(p => p.PaySatus == PaySatus.Completed).Sum(p => p.ProductId) ?? 0
                })
                .ToList();
                //.MapEachTo<UserModel>();
            return users;
        }

        public void ChangeUserStatus(int userId, UserStatus newStatus)
        {
            var user = _repository.Find<AppUser>(userId);
            if (user == null)
                return;
            user.Status = newStatus;
            _repository.SaveChanges();
        }

        public UsersStatistic GetUsersStatistic()
        {
            //var stat = _repository.Queryable<AppUser>()
            //    .Select(u => )
            var userStatistic = new UsersStatistic();
            userStatistic.UsersCount = _repository.Queryable<AppUser>().Count(u => u.PhoneNumberConfirmed);
            userStatistic.PayedUsers = _repository.Queryable<AppUser>().Count(u => u.Profile.IsPaid);

            userStatistic.PayedMounths = _repository
                .Queryable<Pay>()
                .Where(p => p.PaySatus == PaySatus.Completed)
                .Sum(p => p.ProductId);

            userStatistic.AverageMounths = _repository
                .Queryable<Pay>()
                .Where(p => p.PaySatus == PaySatus.Completed)
                .Average(p => p.ProductId);
            return userStatistic;
        }
    }
}