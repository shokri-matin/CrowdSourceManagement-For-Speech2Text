using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using SegamApp.Controllers;

namespace SegamApp.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : BaseController
    {
        SegamDBContext db = null;
        public HomeController()
        {
            db = new SegamDBContext();
        }

        public ActionResult Index()
        {
            int userid = CurrentUser.PersonID;

            List<Task> all_tasks = (from a in db.Tasks where a.PersonID == userid select a).ToList();

            int compeleted_tasks = (from a in all_tasks
                                    where a.Status == (int)Task_Status.Completed
                                    select a).Count();

            int withdraw_tasks = (from a in all_tasks
                                  where a.Status == (int)Task_Status.Withdraw
                                  select a).Count();

            int inprogress_tasks = (from a in all_tasks
                                    where a.Status == (int)Task_Status.InProgress
                                    select a).Count();


            int comoeleted_files = (from a in db.Tasks
                                    join b in db.AllocatedFiles
                                    on a.TaskID equals b.TaskID
                                    where a.PersonID == userid &&
                                    b.IsSubmited == true
                                    select b).Count();


            List<AllocatedFile> inprogress_files = (from a in db.Tasks
                                                    join b in db.AllocatedFiles
                                                    on a.TaskID equals b.TaskID
                                                    where a.PersonID == userid &&
                                                    a.Status == (int)Task_Status.InProgress
                                                    select b).ToList();

            int total_inprogress = inprogress_files.Count;

            int total_submited_inprogress = (from a in inprogress_files where a.IsSubmited == true select a).Count();

            float progress = 0;
            if (total_inprogress > 0)
                progress = ((float)total_submited_inprogress / (float)total_inprogress) * 100;


            List<PersonActivityLog> personActivityLogs = (from t in db.PersonActivityLogs
                                                          where t.PersonID == userid && t.IsDeleted == false
                                                          orderby t.LoginTime descending
                                                          select t).Take(5).ToList();


            VMUserHomeProfile vMUserHomeProfile = new VMUserHomeProfile()
            {
                UserID = userid,
                CompeletedTasks = compeleted_tasks,
                InProgressTasks = inprogress_tasks,
                TotalCompeleteFile = comoeleted_files,
                WithdrawTasks = withdraw_tasks,
                ProgressActiveTask = (int)progress,
                personActivityLogs = personActivityLogs
            };

            return View(vMUserHomeProfile);
        }

        public ActionResult TaskRequest()
        {
            int userid = CurrentUser.PersonID;
            int? ownerid = CurrentUser.CreatorId;

            List<Guid> speeches = db.SpeechFiles.Where(a => a.IsActive == true && a.CreatorId == ownerid).OrderBy(a => a.SequenceID)
                                                .Select(a => a.FileID)
                                                .Except(db.AllocatedFiles.Select(a => a.FileID)).Take(100).ToList();
            if (speeches.Count > 0)
            {
                Task t = db.Tasks.Add(new Task()
                {
                    PersonID = userid,
                    StartTaskDate = DateTime.Now,
                    EndTaskDate = DateTime.Now.AddDays(30),
                    Status = 0
                });

                foreach (var file in speeches)
                {
                    db.AllocatedFiles.Add(new AllocatedFile()
                    {
                        FileID = file,
                        IsSubmited = false,
                        SubmitedTime = null,
                        TaskID = t.TaskID,
                        Text = ""
                    });
                }

                db.SaveChanges();
            }

            else { TempData["Lack_of_file"] = "به دلیل نبود فایل در مخزن فعلا امکان انجام درخواست شما امکان پذیر نمی باشد. لطفا با مدیر سیستم تماس بگیرید"; }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
    }
}