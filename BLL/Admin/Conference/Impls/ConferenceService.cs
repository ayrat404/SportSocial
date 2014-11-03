using System.Collections.Generic;
using BLL.Admin.Conference.ViewModels;
using BLL.Infrastructure.Map;
using DAL.DomainModel.EnumProperties;
using DAL.Repository.Interfaces;

namespace BLL.Admin.Conference.Impls
{
    class ConferenceService : IConferenceService
    {
        private readonly IRepository _repository;

        public ConferenceService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ConfModel> GetAll()
        {
            var conferences = _repository.GetAll<DAL.DomainModel.Conference>();
            return conferences.MapEachTo<ConfModel>();
        }

        public void Create(CreateConfModel model)
        {
            var conference = new DAL.DomainModel.Conference();
            model.MapTo(conference);
            _repository.Add(conference);
            _repository.SaveChanges();
        }

        public void Edit(ConfModel model)
        {
            var conf = _repository.Find<DAL.DomainModel.Conference>(model.Id);
            if (conf != null) //TODO если не найден нужно об хтом сообщать
            {
                conf = model.MapTo(conf);
                _repository.SaveChanges();
            }
        }

        public void ChangeStatus(int id, ConfStatus status)
        {
            var conf = _repository.Find<DAL.DomainModel.Conference>(id);
            if (conf != null)
            {
                conf.Status = status;
                _repository.Update(conf);
                _repository.SaveChanges();
            }
        }

        public ConfModel GetConf(int id)
        {
            var conf = _repository.Find<DAL.DomainModel.Conference>(id);
            if (conf != null)
                return conf.MapTo<ConfModel>();
            return null;
        }
    }
}