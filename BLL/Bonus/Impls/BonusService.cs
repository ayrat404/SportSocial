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
        private readonly string basePath = @"\\Videos\\course1\\";
        private readonly string basePathImgs = @"/Content/images/courses/imgs/";
       
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
            return GetMounths() >= 1;
        }

        public List<BonusVideoModel> GetBonusVideos()
        {
            var mounths = GetMounths();
            mounths = mounths*2;//%2 == 0 ? mounths : mounths - 1;
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
            return videoId <= mounths * 2;
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
            var imgsPath = Path.Combine(basePath, "imgs");
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 1,
                Title = "1 видео",
                ImageUrl = basePathImgs + "1.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 2,
                Title = "2 видео",
                ImageUrl = basePathImgs + "2.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 3,
                Title = "3 видео",
                ImageUrl = basePathImgs + "3.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 4,
                Title = "4 видео",
                ImageUrl = basePathImgs + "4.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 5,
                Title = "5 видео",
                ImageUrl = basePathImgs + "5.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 6,
                Title = "5 видео",
                ImageUrl = basePathImgs + "6.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 7,
                Title = "7 видео",
                ImageUrl = basePathImgs + "7.jpg",
                IsActive = false
            });
            _bonusVideos.Add(new BonusVideoModel
            {
                Id = 8,
                Title = "8 видео",
                ImageUrl = basePathImgs + "8.jpg",
                IsActive = false
            });
        }


    }
}