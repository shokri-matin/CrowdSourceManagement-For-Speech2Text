using DataLayer;
using SegamApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserProfileController : BaseController
    {
        SegamDBContext db = null;
        public UserProfileController()
        {
            db = new SegamDBContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pass1, string pass2)
        {
            if (ModelState.IsValid)
            {
                if (pass1 != pass2)
                {
                    ModelState.AddModelError("", "رمز عبور یکسان نیست");
                }
                else if(string.IsNullOrEmpty(pass1) || string.IsNullOrEmpty(pass2))
                {
                    ModelState.AddModelError("", "رمز عبور نمیتواند خالی باشد");
                }
                else
                {
                    int userId = CurrentUser.PersonID;
                    var person = db.Persons.Find(userId);
                    person.Password = pass1;
                    db.SaveChanges();

                    ViewBag.PassChangeSucc = "رمز عبور با موفقیت تغییر کرد";
                }
            }
            return View();
        }
    }
}