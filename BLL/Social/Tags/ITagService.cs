using System.Collections.Generic;

namespace BLL.Social.Tags
{
    public interface ITagService
    {
        IEnumerable<string> GetTags(string query);
        void AddTags(int id, List<string> addedTags);
    }
}