using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using SegamApp.Controllers;
namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HelperController : BaseController
    {
        public string GetNameByUserName(string username)
        {
            var user = "";
            using (SegamDBContext context = new SegamDBContext())
            {
                user = (from u in context.Persons
                        where u.PersonEmail == username
                        select u).FirstOrDefault().PersonName;
            
                return user;
            }
        }
    }
}