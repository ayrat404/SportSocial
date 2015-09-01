using System.Collections.Generic;

namespace BLL.Tags
{
    public interface ITagService
    {
        IEnumerable<string> GetTags(string query);
    }
}