﻿using System;
using System.Net;
using System.Net.Mail;
using BLL.Common.Objects;
using BLL.Common.Services.CurrentUser;
using BLL.Core.Services.Support.Objects;
using NLog;

namespace BLL.Core.Services.Support
{
    public interface ISupportService
    {
        ServiceResult Send(FeedBackModel feedBack);
    }

    public class SupportService : ISupportService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ICurrentUser _currentUser;

        public SupportService(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public ServiceResult Send(FeedBackModel feedBackModel)
        {
            var userPhone = !_currentUser.IsAnonimous ? _currentUser.Phone : "";
            string from = "admin@fortress.club";
            string to = "support@fortress.club";
            string passwd = "Qli4$ton";
            string subject = string.Format("Отзыв от пользователя {0}({1}, {2})",
                feedBackModel.Name, feedBackModel.Email, userPhone);
            string body = feedBackModel.Problem;
            var mailClient = new SmtpClient
            {
                EnableSsl = true,
                Host = "smtp.yandex.ru",
                Port = 25,
                Credentials = new NetworkCredential(from, passwd),
            };
            try
            {
                using (var msg = new MailMessage(from, to, subject, body))
                {
                    mailClient.Send(msg);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Ошибка отправки письма {0}, {1}, {2}, msg=\"{3}\"", feedBackModel.Name,
                    feedBackModel.Email, userPhone, feedBackModel.Problem);
                _logger.Error(ex, msg);
            }
            return ServiceResult.SuccessResult();
        }
    }
}