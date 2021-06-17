using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAppController : Controller
    {
        // GET: Admin/UserApp
        public ActionResult Index()
        {
            return View();
        }
    }
}