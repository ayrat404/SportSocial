using System;
using BLL.Admin.Conference.ViewModels;
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

        public ConferencesList GetAll()
        {
            throw new NotImplementedException();
        }
    }
}