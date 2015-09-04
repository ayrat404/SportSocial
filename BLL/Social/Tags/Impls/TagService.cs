using System.Collections.Generic;
using System.Linq;
using DAL.DomainModel;
using DAL.DomainModel.JournalEntities;
using DAL.Repository.Interfaces;

namespace BLL.Social.Tags.Impls
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

        public void AddTags(int id, List<string> addedTags)
        {
            var tags = _repository.Queryable<Tag>()
                .Where(t => addedTags.Contains(t.Label))
                .ToList();
            foreach (var addedTag in addedTags)
            {
                if (!tags.Any(t => t.Label == addedTag))
                {
                    var tag = new Tag { Label = addedTag };
                    _repository.Add(tag);
                    tags.Add(tag);
                }
            }
            _repository.SaveChanges();
            foreach (var tag in tags)
            {
                var journalTag = new JournalTag { TagId = tag.Id, JournalId = id };
                _repository.Add(journalTag);
            }
            _repository.SaveChanges();
        }
    }
}