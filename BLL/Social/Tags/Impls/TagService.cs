using System.Collections.Generic;
using System.Data.Entity;
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
            query = query.ToLower();
            return _repository.Queryable<Tag>()
                .Where(t => t.Label.ToLower().StartsWith(query))
                .ToList()
                .Select(t => t.Label);
        }

        public void AddTags(int id, List<string> addedTags)
        {
            var existingJornalTags = _repository.Queryable<JournalTag>()
                .Include(t => t.Tag)
                .Where(t => t.JournalId == id)
                .ToList();
            addedTags = addedTags.Where(a => existingJornalTags.All(t => t.Tag.Label != a)).ToList();
            if (!addedTags.Any())
            {
                return;
            }
            var tags = _repository.Queryable<Tag>()
                .Where(t => addedTags.Contains(t.Label))
                .ToList();
            foreach (var addedTag in addedTags)
            {
                if (tags.All(t => t.Label != addedTag))
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