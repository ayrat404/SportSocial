using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Social.Journals;
using BLL.Social.Journals.Objects;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/journals")]
    public class JournalController : BaseApiController
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        public ApiResult Create(JournalVm journal)
        {
            if (ModelState.IsValid)
            {
                _journalService.CreateJournal(journal);
            }
            return ModelStateErrors();
        }
    }
}
