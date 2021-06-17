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
    public class MonitoringController : BaseController
    {
        SegamDBContext db = null;
        
        public MonitoringController()
        {
            db = new SegamDBContext();
        }

        public ActionResult Index(string personId)
        {
            int? ownerId = CurrentUser.PersonID;
            var persons = (from p in db.Persons.
                            Where(x => x.IsDeleted == false && x.CreatorId == ownerId && x.RoleID != (int)(RolesStatus.Admin))
                           select p).ToList();

            List<SelectListItem> plist = new List<SelectListItem>();
            foreach (var person in persons)
            {
                SelectListItem p = null;
                if (personId == person.PersonID.ToString())
                    p = new SelectListItem() { Value = person.PersonID.ToString(), Text = person.PersonName, Selected = true };
                else
                    p = new SelectListItem() { Value = person.PersonID.ToString(), Text = person.PersonName };

                plist.Add(p);
            }

            ViewBag.Persons = plist;

            MVPagination mvp = new MVPagination();

            int id = 0;
            if (personId == null)
                id = int.Parse(plist.First().Value);
            else
                id = int.Parse(personId);

            mvp.MVTask = (from a in db.Tasks
                          join b in db.AllocatedFiles
                          on a.TaskID equals b.TaskID
                          join c in db.SpeechFiles.Where(p => p.IsActive == true)
                          on b.FileID equals c.FileID
                          where a.PersonID == id &&
                                 b.IsSubmited == true && b.IsSupervised == false
                          select new MVTask()
                          {
                              TaskID = a.TaskID,
                              FileID = b.FileID,
                              SequenceId = c.SequenceID,
                              CreatorId = c.CreatorId,
                              SuggestedText = c.SuggestedText,
                              Text = b.Text,
                              FileType = c.FileType,
                              FileDuration = c.FileDuration,
                              IsSubmited = b.IsSubmited

                          }).OrderBy(p => p.SequenceId).ToList();

            return View(mvp);
        }

        public JsonResult Submit(string id, string text)
        {
            Guid guid = Guid.Parse(id);
            var file = (from a in db.AllocatedFiles where a.FileID == guid select a).FirstOrDefault();
            file.IsSupervised = text != "";
            file.Text = text;

            db.SaveChanges();

            return Json(new { FileId = id, IsEmpty = !file.IsSupervised });
        }
    }
}