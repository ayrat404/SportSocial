using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Web;
using BLL.Bonus.Objects;
using BLL.Common.Services.CurrentUser;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Bonus.Impls
{
    public class BonusService : IBonusService
    {
        private readonly string basePath = @"\\videos\\";
        
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        private readonly List<BonusVideoModel> _bonusVideos;

        public BonusService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
            _bonusVideos = new List<BonusVideoModel>();
            AddVideo();
        }

        public bool HaveAccess()
        {
            return GetMounths() >= 2;
        }

        public List<BonusVideoModel> GetBonusVideos()
        {
            var mounths = GetMounths();
            mounths = mounths%2 == 0 ? mounths : mounths - 1;
            foreach (var video in _bonusVideos)
            {
                if (video.Id <= mounths)
                {
                    video.IsActive = true;
                    continue;
                }
                break;
            }
            return _bonusVideos;
        }

        public bool CanViewVideo(int videoId)
        {
            var mounths = GetMounths();
            return videoId <= mounths;
        }

        public string GetVideoFilePAth(string fileName)
        {
            return HttpContext.Current.Server.MapPath(Path.Combine(basePath, fileName));
        }

        private int GetMounths()
        {
            var pays = _repository.Queryable<Pay>()
                .Where(p => p.PaySatus == PaySatus.Completed 
                         && p.UserId == _currentUser.UserId
                         && p.ProductId >=1 && p.ProductId <= 12)
                .ToList();
            return pays.Sum(p => p.ProductId);
        }

        private void AddVideo()
        {
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 1,
                Title = "1 видео",
                ImageUrl = "/storage/v/1.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 2,
                Title = "2 видео",
                ImageUrl = "/storage/v/2.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 3,
                Title = "3 видео",
                ImageUrl = "/storage/v/3.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 4,
                Title = "4 видео",
                ImageUrl = "/storage/v/4.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 5,
                Title = "5 видео",
                ImageUrl = "/storage/v/5.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 6,
                Title = "5 видео",
                ImageUrl = "/storage/v/6.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 7,
                Title = "7 видео",
                ImageUrl = "/storage/v/7.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 8,
                Title = "8 видео",
                ImageUrl = "/storage/v/8.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 9,
                Title = "9 видео",
                ImageUrl = "/storage/v/9.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 10,
                Title = "10 видео",
                ImageUrl = "/storage/v/10.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 10,
                Title = "10 видео",
                ImageUrl = "/storage/v/10.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 11,
                Title = "11 видео",
                ImageUrl = "/storage/v/11.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 12,
                Title = "12 видео",
                ImageUrl = "/storage/v/12.jpg",
                IsActive = false
            });
        }


    }
}