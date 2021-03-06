﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BLL.Admin.Conference.ViewModels;
using BLL.Common.Helpers;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Infrastructure.Map;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;
using Knoema.Localization;

namespace BLL.Admin.Conference.Impls
{
    public class ConferenceService : IConferenceService
    {
        private readonly IRepository _repository;
        private readonly ICurrentUser _currentUser;

        public ConferenceService(IRepository repository, ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public IEnumerable<ConfModel> GetAll()
        {
            var conferences = _repository.GetAll<DAL.DomainModel.ConferenceEntities.Conference>();
            return conferences.MapEachTo<ConfModel>();
        }

        public ServiceResult Create(CreateConfModel model)
        {
            var result = new ServiceResult {Success = true};
            if (YoutubeUrlHelper.UrlIsValid(model.Url))
            {
                model.Url = YoutubeUrlHelper.EmbeddedYoutubeUrl(model.Url);
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "Не валидный формат ссылки".Resource(this);
                return result;
            }
            var conference = model.MapTo<DAL.DomainModel.ConferenceEntities.Conference>();
            conference.UserId = _currentUser.UserId;
            //var youtubeUrl = new Uri("//www.youtube.com/embed/Rqp8DltI858");
            _repository.Add(conference);
            _repository.SaveChanges();
            return new ServiceResult {Success = true};
        }

        public ServiceResult Edit(ConfModel model)
        {
            var result =  new ServiceResult {Success = true};
            var conf = _repository
                .Find<DAL.DomainModel.ConferenceEntities.Conference>(model.Id);
            if (conf != null) //TODO если не найден нужно об хтом сообщать
            {
                if (!string.IsNullOrEmpty(model.Url) && conf.Url != model.Url && !model.Url.Contains("youtube.com/embed"))
                {
                    if (YoutubeUrlHelper.UrlIsValid(model.Url))
                    {
                        model.Url = YoutubeUrlHelper.EmbeddedYoutubeUrl(model.Url);
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Не валидный формат ссылки".Resource(this);
                        return result;
                    }
                }
                conf = model.MapTo(conf);
                if ((conf.Status == ConfStatus.Finish || conf.Status == ConfStatus.Remove) && conf.Date > DateTime.Now)
                {
                    conf.Status = ConfStatus.Created;
                }
                _repository.SaveChanges();
            }
            return result;
        }

        public ServiceResult ChangeStatus(int id, ConfStatus status)
        {
            var conf = _repository.Find<DAL.DomainModel.ConferenceEntities.Conference>(id);
            if (conf != null)
            {
                conf.Status = status;
                _repository.Update(conf);
                _repository.SaveChanges();
            }
            return new ServiceResult {Success = true};
        }

        public ConfModel GetConf(int id)
        {
            var conf = _repository.Find<DAL.DomainModel.ConferenceEntities.Conference>(id);
            if (conf != null)
                return conf.MapTo<ConfModel>();
            return null;
        }

        public ProcessConfModel GetInProcessConf(int id)
        {
            var conf = _repository
                .Queryable<DAL.DomainModel.ConferenceEntities.Conference>()
                .Where(c => c.Id == id)
                .Include(c => c.Comments) 
                .Include(c => c.Comments.Select(com => com.User)) 
                .Include(p => p.Comments.Select(c => c.CommentFor))
                .SingleOrDefault();
            if (!(conf != null
                && (conf.Status == ConfStatus.Process || conf.Date < DateTime.Now)
                && conf.Status != ConfStatus.Remove))
            {
                conf = null;
            }
            if (conf != null)
                return conf.MapTo<ProcessConfModel>();
            return null;
        }

        public ConfModel GetLastConf()
        {
            var confs = _repository
                .Queryable<DAL.DomainModel.ConferenceEntities.Conference>()
                .Where(c => c.Status == ConfStatus.Process || c.Status == ConfStatus.Created)
                .OrderBy(c => c.Created)
                .AsNoTracking()
                .ToList()
                .MapEachTo<ConfModel>();
            if (confs.Any())
            {
                var resultConf = confs.FirstOrDefault(c => c.Status == ConfStatus.Process);
                if (resultConf != null)
                {
                    return resultConf.MapTo<ConfModel>();
                }
                return confs.First().MapTo<ConfModel>();
            }
            return null;
        }


    }
}