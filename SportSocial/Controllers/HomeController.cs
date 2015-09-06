using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using BLL.Common.Services.CurrentUser;
using BLL.Core.Services.Support.Objects;
using NLog;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class HomeController : SportSocialControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [CustomAntiForgeryValidator]
        public ActionResult Feedback(FeedBackModel feedBackModel)
        {
            var userPhone = !_currentUser.IsAnonimous ? _currentUser.Phone : "";
            if (ModelState.IsValid)
            {
                string from = "admin@fortress.club";
                string to = "support@fortress.club";
                string passwd = "Qli4$ton";
                string subject = string.Format("Отзыв от пользователя {0}({1}, {2})", 
                    feedBackModel.Name, feedBackModel.Email, userPhone) ;
                string body = feedBackModel.Text;
                var mailClient = new SmtpClient
                {
                    EnableSsl = true,
                    Host = "smtp.yandex.ru",
                    Port = 25,
                    Credentials = new NetworkCredential(from, passwd),
                };
                try
                {
                    var msg = new MailMessage(from, to, subject, body);
                    mailClient.Send(msg);
                }
                catch (Exception ex)
                {
                    string msg = string.Format("Ошибка отправки письма {0}, {1}, {2}, msg=\"{3}\"", feedBackModel.Name,
                        feedBackModel.Email, userPhone, feedBackModel.Text);
                    _logger.Error(msg, ex);
                }
                return Json(new {Success = true});
            }
            return Json(new {Success = false, ErrorMessage = GetModelStateErrors()});
        }

    }
}