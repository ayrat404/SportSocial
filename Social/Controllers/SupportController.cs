using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Core.Services.Support;
using BLL.Core.Services.Support.Objects;
using DAL.Migrations;
using Social.Models;

namespace Social.Controllers
{
    public class SupportController : BaseApiController
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        public ApiResult Send(FeedBackModel feedBack)
        {
            if (ModelState.IsValid)
            {
                _supportService.Send(feedBack);
            }
            return ModelStateErrors();
        }
    }
}
