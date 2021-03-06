﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Social.Journals;
using BLL.Social.Journals.Objects;
using Social.Models;

namespace Social.Controllers
{
    [RoutePrefix("api/journal")]
    public class JournalController : BaseApiController
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }


        [Authorize]
        [HttpPost]
        [Route("")]
        public ApiResult Create(JournalVm journal)
        {
            if (ModelState.IsValid)
            {
                return ApiResult(_journalService.CreateJournal(journal));
            }
            return ModelStateErrors();
        }

        [Route("")]
        [HttpGet]
        public ApiResult GetJournal(int id)
        {
            var journal = _journalService.GetJournal(id);
            return ApiResult(journal);
        }

        [Route("")]
        [HttpPut]
        public ApiResult EditJournal(JournalVm journal)
        {
            return ApiResult(_journalService.EditJournal(journal));
        }

        [Route("")]
        [HttpDelete]
        public ApiResult DeleteJournal(int id)
        {
            return ApiResult(_journalService.DeleteJournal(id));
        }

        [Route("~/api/records")]
        [HttpGet]
        public ApiResult GetJournals(int authorId, int count = 20, int page = 1)
        {
            return ApiResult(_journalService.GetJournals(authorId, page, count));
        }
    }
}
