using SegamApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegamApp.Areas.Daemon.Controllers
{
    [Authorize(Roles = "Daemon")]
    public class HomeController : BaseController
    {
        // GET: Daemon/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}