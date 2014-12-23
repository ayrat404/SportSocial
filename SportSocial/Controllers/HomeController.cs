using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using SportSocial.Controllers.Base;

namespace SportSocial.Controllers
{
    public class HomeController : SportSocialControllerBase
    {

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
            if (ModelState.IsValid)
            {
                string from = "admin@fortress.club";
                string to = "support@fortress.club";
                string passwd = "Qli4$ton";
                string subject = "Отзыв от пользователя " + feedBackModel.Name;
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
                catch (Exception e)
                {
                    //TODO логирование
                }
                return Json(new {Success = true});
            }
            return Json(new {Success = false, ErrorMessage = GetModelStateErrors()});
        }

    }

    public class FeedBackModel
    {
        [EmailAddress(ErrorMessage = "Неправильное написание электронной почты")]
        [Required(ErrorMessage = "Укажите электронную почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите текст")]
        public string Text { get; set; }
    }
}