using System.Collections.Generic;
using BLL.Common.Objects;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Journals
{
    public interface IJournalService
    {
        ServiceResult<JournalDisplayVm> CreateJournal(JournalVm journal);
        IEnumerable<JournalPreviewVm> GetJournals(int userId);
        JournalDisplayVm GetJournal(int journalId);
        ServiceResult EditJournal(JournalVm journalModel);
        ServiceResult DeleteJournal(int id);
    }
}