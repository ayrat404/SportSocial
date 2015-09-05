using BLL.Common.Objects;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Journals
{
    public interface IJournalService
    {
        ServiceResult CreateJournal(JournalVm journal);
    }
}