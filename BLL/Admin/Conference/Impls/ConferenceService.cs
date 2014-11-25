using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using BLL.Admin.Conference.ViewModels;
using BLL.Infrastructure.Map;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Admin.Conference.Impls
{
    public class ConferenceService : IConferenceService
    {
        private readonly IRepository _repository;

        public ConferenceService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ConfModel> GetAll()
        {
            var conferences = _repository.GetAll<DAL.DomainModel.ConferenceEntities.Conference>();
            return conferences.MapEachTo<ConfModel>();
        }

        public void Create(CreateConfModel model)
        {
            var conference = new DAL.DomainModel.ConferenceEntities.Conference();
            var url = new Uri(model.Url);
            var videoId = HttpUtility.ParseQueryString(url.Query)["v"];
            string resultUrl = "//www.youtube.com/embed/" + videoId;
            model.MapTo(conference);
            conference.Url = resultUrl;
            //var youtubeUrl = new Uri("//www.youtube.com/embed/Rqp8DltI858");
            _repository.Add(conference);
            _repository.SaveChanges();
        }

        public void Edit(ConfModel model)
        {
            var conf = _repository.Find<DAL.DomainModel.ConferenceEntities.Conference>(model.Id);
            if (conf != null) //TODO если не найден нужно об хтом сообщать
            {
                var youtubeUrl = new Uri("//www.youtube.com/embed/Rqp8DltI858");
                conf = model.MapTo(conf);
                var url = new Uri(conf.Url);
                _repository.SaveChanges();
            }
        }

        public void ChangeStatus(int id, ConfStatus status)
        {
            var conf = _repository.Find<DAL.DomainModel.ConferenceEntities.Conference>(id);
            if (conf != null)
            {
                conf.Status = status;
                _repository.Update(conf);
                _repository.SaveChanges();
            }
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
                .Where(c => c.Id == id && c.Status == ConfStatus.Process)
                .Include(c => c.Comments) 
                .SingleOrDefault();
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