using DataLayer;
using SegamApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SegamApp.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class TaskController : BaseController
    {
        SegamDBContext db = null;
        public TaskController()
        {
            db = new SegamDBContext();
        }

        public ActionResult Index(int page = 1, int number = 10)
        {
            int userid = CurrentUser.PersonID;

            MVPagination mvp = new MVPagination();

            mvp.Count = (from a in db.Tasks
                         join b in db.AllocatedFiles
                         on a.TaskID equals b.TaskID
                         join c in db.SpeechFiles
                         on b.FileID equals c.FileID
                         where a.PersonID == userid &&
                         a.Status == (int)Task_Status.InProgress
                         select a).Count();



            mvp.MVTask = (from a in db.Tasks
                          join b in db.AllocatedFiles
                          on a.TaskID equals b.TaskID
                          join c in db.SpeechFiles.Where(p=> p.IsActive == true)
                          on b.FileID equals c.FileID
                          where a.PersonID == userid &&
                          a.Status == (int)Task_Status.InProgress
                          select new MVTask()
                          {
                              TaskID = a.TaskID,
                              FileID = b.FileID,
                              SequenceId = c.SequenceID,
                              CreatorId = c.CreatorId,
                              SuggestedText = c.SuggestedText,
                              Text = b.Text,
                              Gender = b.Gender,
                              FileType = c.FileType,
                              FileDuration = c.FileDuration,
                              IsSubmited = b.IsSubmited,
                              IsSupervised = b.IsSupervised

                          }).OrderBy(p => p.SequenceId).Skip((page - 1) * number).Take(number).ToList();


            mvp.CurrentPage = page;

            return View(mvp);
        }

        public JsonResult Submit(string id, string text, int gender)
        {

            Guid guid = Guid.Parse(id);
            var file = (from a in db.AllocatedFiles where a.FileID == guid select a).FirstOrDefault();
            file.IsSubmited = text != "";

            DateTime? publishedTime = null;
            if (file.IsSubmited) { publishedTime = DateTime.Now; }

            file.SubmitedTime = publishedTime;
            file.Text = text;

            file.Gender = gender;

            db.SaveChanges();


            return Json(new { FileId = id, IsEmpty = !file.IsSubmited });

            //return RedirectToAction("/Index", new { area = "User" });
        }

        public ActionResult EndTask()
        {
            TempData["errMessage"] = "";

            int userid = (from a in db.Persons
                          where
                             a.PersonEmail == User.Identity.Name
                          select a.PersonID).FirstOrDefault();


            int inprogress_files = (from a in db.Tasks
                                    join b in db.AllocatedFiles
                                    on a.TaskID equals b.TaskID
                                    where a.PersonID == userid && b.IsSubmited == false
                                                               && a.Status == (int)Task_Status.InProgress
                                    select b).Count();


            if (inprogress_files <= 0)
            {
                Task t = (from a in db.Tasks where a.PersonID == userid && a.Status == 0 select a).FirstOrDefault();
                t.Status = (int)Task_Status.Completed;
                db.SaveChanges();
            }
            else
            {
                TempData["errMessage"] = "وظیفه جاری ناتمام می باشد. لطفا آن را به پایان رسانید سپس درخواست وظیفه جدید فرمایید";
            }


            return RedirectToAction("Index", "Task", new { area = "User" });
        }

        ~TaskController()
        {
            db.Dispose();
        }
    }
}