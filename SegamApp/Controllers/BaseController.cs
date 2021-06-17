using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataLayer;

namespace SegamApp.Controllers
{
    public class BaseController : Controller
    {
        SegamDBContext db = null;
        public Person CurrentUser { get; set; }
        public BaseController()
        {
            db = new SegamDBContext();
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}

            if (IsAjax(filterContext))
            {
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                //Normal Exception
                //So let it handle by its default ways.
                base.OnException(filterContext);

            }

            var currentController = (string)filterContext.RouteData.Values["controller"];
            var currentAction = (string)filterContext.RouteData.Values["action"];

            db.ExceptionsLogging.Add(new ExceptionLogging
            {
                PersonID = GetPersonID(User.Identity.Name),
                ControllerName = currentController != null ? currentController: "",
                ActionName = currentAction != null ? currentAction : "",
                Exception = filterContext.Exception != null ? filterContext.Exception.Message : "",
                InnerException = filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message : "",
                LogDate = DateTime.Now
            });

            db.SaveChanges();

            base.OnException(filterContext);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = requestContext.HttpContext.User.Identity.Name;
                CurrentUser = db.Persons.Where(p => p.PersonEmail == userName && p.IsDeleted == false).FirstOrDefault();
            }
        }
        public int GetPersonID(string username)
        {
            var person = db.Persons.Where(pt => pt.PersonEmail == username).FirstOrDefault();
            if (person == null)
                return 1;
            else
                return person.PersonID;
        }
    }
}