using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;

namespace SegamApp.Controllers
{
    public class LoginController : BaseController
    {
        SegamDBContext db = null;
        public LoginController()
        {
            db = new SegamDBContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(VMLogin info)
        {
            if (ModelState.IsValid)
            {
                var person = db.Persons.Where(p => p.PersonEmail == info.PersonEmail &&
                                     p.Password == info.Password &&
                                     p.IsDeleted == false).FirstOrDefault();

                if (person == null)
                {
                    ModelState.AddModelError("PersonEmail", "کاربری یافت نشد");
                }
                else
                {

                    db.PersonActivityLogs.Add(new PersonActivityLog()
                    {
                        PersonID = person.PersonID,
                        LoginTime = DateTime.Now,
                        ActivityStatus = (int)ActivityStatus.Login,
                        IsDeleted = false,
                    });

                    db.SaveChanges();

                    string RoleName = person.Role.RoleName;

                    FormsAuthentication.SetAuthCookie(info.PersonEmail, info.Remeber);
                    if (RoleName == "Admin")
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    else if(RoleName == "User")
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    else
                        return RedirectToAction("Index", "Home", new { area = "Daemon" });
                }

            }
            return View(info);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            var person = (from a in db.Persons where a.PersonEmail == User.Identity.Name select a).FirstOrDefault();

            db.PersonActivityLogs.Add(new PersonActivityLog()
            {
                PersonID = person.PersonID,
                LoginTime = DateTime.Now,
                ActivityStatus = Convert.ToInt32(ActivityStatus.Logout),
                IsDeleted = false,
            });
            db.SaveChanges();
            return Redirect("/Login");
        }
    }
}