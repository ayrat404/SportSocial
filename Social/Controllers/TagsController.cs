using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Social.Tags;
using Social.Models;

namespace Social.Controllers
{
    public class TagsController : BaseApiController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("~/api/sport_themes")]
        public ApiResult<IEnumerable<string>>  GetTags(string query)
        {
            var tags = _tagService.GetTags(query);
            return new ApiResult<IEnumerable<string>>
            {
                Success = true,
                Data = tags
            };
        }
    }
}
