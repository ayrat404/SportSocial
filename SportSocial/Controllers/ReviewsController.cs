﻿using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BLL.Blog.ViewModels;
using BLL.Common.Services.CurrentUser;
using BLL.Feedbacks;
using BLL.Feedbacks.Objects;
using DAL.DomainModel;
using DAL.DomainModel.EnumProperties;
using PagedList;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    //[Authorize]
    public class ReviewsController :SportSocialControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        private const int PageSize = 10;

        public ReviewsController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, FeedbackSortType sort = FeedbackSortType.Date)
        {
            ViewBag.Sort = sort;
            var feeds = _feedbackService.GetFeedbacks(PageSize, sort, page);
            var pageList = new StaticPagedList<FeedbackPreviewModel>(feeds.PostPreview, page, PageSize,
                feeds.PageInfo.Count);
            return View(pageList);
        }

        
        [HttpPost]
        [CustomAntiForgeryValidator]
        [Authorize]
        public ActionResult Add(CreateFeedbackModel feedbackModel)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.AddFeedback(feedbackModel);
                return RedirectToAction("Index");
            }
            return View();
        }
	}
}