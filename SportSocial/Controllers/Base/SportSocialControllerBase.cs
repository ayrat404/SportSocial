﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using BLL.Common.Helpers;
using BLL.Common.Services.CurrentUser;
using Knoema.Localization;
using SportSocial.Infrastructure;

namespace SportSocial.Controllers.Base
{
    public abstract class SportSocialControllerBase: Controller
    {
        protected readonly ICurrentUser CurrentUser;

        public SportSocialControllerBase(): this(DependencyResolver.Current.GetService<ICurrentUser>()) { }

        public SportSocialControllerBase(ICurrentUser currentUser)
        {
            CurrentUser = currentUser;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CultureInfo currentUserCulture;
            string currentCultureString = LanguageHelper.GetCurrentCulture();
            try
            {
                currentUserCulture = new CultureInfo(currentCultureString);
            }
            catch (CultureNotFoundException ex)
            {
                //TODO логгировать
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                currentUserCulture = currentCultureString.StartsWith("ru") ? new CultureInfo("ru-RU") : new CultureInfo("en-US");
            }
            LocalizationManager.Instance.SetCulture(currentUserCulture);
            Thread.CurrentThread.CurrentCulture = currentUserCulture;
            Thread.CurrentThread.CurrentUICulture = currentUserCulture;
            base.OnActionExecuting(filterContext);
        }

        protected override JsonResult Json(object data, string contentType, 
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected string GetModelStateErrors()
        {
            var errors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                if (value.Errors.Any())
                {
                    var err = value.Errors.GroupBy(e => e.ErrorMessage);
                    errors.AddRange(err.Select(e => e.Key));
                }
            }
            return string.Join(Environment.NewLine, errors);
        }

        protected string RenderPartialToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}