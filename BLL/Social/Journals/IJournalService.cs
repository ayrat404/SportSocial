using System.Collections.Generic;
using BLL.Common.Objects;
using BLL.Social.Achievements.Objects;
using BLL.Social.Journals.Objects;

namespace BLL.Social.Journals
{
    public interface IJournalService
    {
        ServiceResult<JournalDisplayVm> CreateJournal(JournalVm journal);
        PagedListVm<JournalPreviewVm> GetJournals(int userId, int page, int count);
        JournalDisplayVm GetJournal(int journalId);
        ServiceResult EditJournal(JournalVm journalModel);
        ServiceResult DeleteJournal(int id);
    }
}