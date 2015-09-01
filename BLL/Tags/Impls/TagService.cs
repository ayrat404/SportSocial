using System.Collections.Generic;
using System.Linq;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace BLL.Tags.Impls
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;

        public TagService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetTags(string query)
        {
            return _repository.Queryable<Tag>()
                .Where(t => t.Label.ToLower().StartsWith(query))
                .ToList()
                .Select(t => t.Label);
        }
    }
}