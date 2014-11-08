using System;
using System.Globalization;
using System.Web.Mvc;
using BLL.Blog;
using BLL.Blog.ViewModels;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    //[Authorize]
    public class BlogController :SportSocialControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            var blogModel = new CreatePostModel()
            {
                Rubrics = _blogService.GetRubrics(),
            };
            return View(blogModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult New(CreatePostModel createPostModel)
        {
            if (ModelState.IsValid)
                return Json(_blogService.CreatePost(createPostModel));
            return Json(new {success = false});
        }

        public ActionResult Item(int id)
        {
            var post = new BlogPostViewModel()
            {
                Id = 1,
                AuthorId = 100,
                AuthorName = "Василий Иванович",
                Date = DateTime.Now,
                Images = new Images[] { new Images { Id = 2, Url = "/Storage/Images/2.jpg" }},
                Title = "Василий Ивановаич тагает штангу",
                Rating = 123,
                Text = @"Принято считать, что оптимальным временем для тренировки с целью набора мышечной массы является отрезок от 40 минут до полутора часов. В данной статье я попробую максимально наглядно объяснить, чем эти рамки обусловлены
            Выработка гормонов являются одним из важнейших факторов вашего прогресса. Во время силовых тренировок выделяется несколько так называемых «анаболических» (ответственных за рост, прогресс мышечных объёмов) гормонов. К ним относятся: соматотропин (гормон роста), тестостерон и инсулин. Мы не будем отдельно останавливаться на каждом из них и подробно расписывать их функции, но лишь в краткой&
            Принято считать, что оптимальным временем для тренировки с целью набора мышечной массы является отрезок от 40 минут до полутора часов. В данной статье я попробую максимально наглядно объяснить, чем эти рамки обусловлены выработка гормонов являются одним из важнейших факторов вашего прогресса. Во время силовых тренировок выделяется несколько так называемых «анаболических» (ответственных за рост, прогресс мышечных объёмов) гормонов. К ним относятся: соматотропин (гормон роста), тестостерон и инсулин. Мы не будем отдельно останавливаться на каждом из них и подробно расписывать их функции, но лишь в краткой
            Принято считать, что оптимальным временем для тренировки с целью набора мышечной массы является отрезок от 40 минут до полутора часов. В данной статье я попробую максимально наглядно объяснить, чем эти рамки обусловлены
            Мы не будем отдельно останавливаться на каждом из них и подробно расписывать их функции, но лишь в краткой.
            Принято считать, что оптимальным временем для тренировки с целью набора мышечной массы является отрезок от 40 минут до полутора часов. В данной статье я попробую максимально наглядно объяснить, чем эти рамки обусловлены выработка гормонов являются одним из важнейших факторов вашего прогресса. Во время силовых тренировок выделяется несколько так называемых «анаболических» (ответственных за рост, прогресс мышечных объёмов) гормонов. К ним относятся: соматотропин (гормон роста), тестостерон и инсулин. Мы не будем отдельно останавливаться на каждом из них и подробно расписывать их функции, но лишь в краткой.",
                Comments = new Comment[] 
                { 
                    new Comment()
                    {
                        Id = 200,
                        Name = "Иван",
                        Avatar = "/Storage/Images/3.jpg",
                        Text = "Статья говно",
                        Date = DateTime.Now,
                    }, 
                }
            };
            return View(post);
        }

        [HttpPost]
        public ActionResult Rating(BlogRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blogService.Rait(model);
                return Json(new {Success = true});
            }
            return Json(new {Success = false});
        }
	}
}