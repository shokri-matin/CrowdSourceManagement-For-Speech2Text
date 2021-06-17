using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using SegamApp.Controllers;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        SegamDBContext db = null;
        public HomeController()
        {
            db = new SegamDBContext();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            /*
            var compeleted_tasks = (from p in db.Persons
                         join t in db.Tasks
                            on p.PersonID equals t.PersonID
                                where p.IsDeleted == false && t.Status == 1
                                    group p by p.PersonID into g
                         select new
                         {
                             person = g.Key,
                             count = g.Count()
                         });

            */
            int ownerId = CurrentUser.PersonID;

            var persons = db.Persons.Where(p => p.IsDeleted == false && p.CreatorId == ownerId).ToList();

            if (persons.Count <= 0)
                return View("Empty");

            List<VMPersonStatus> vMPersonStatuses = new List<VMPersonStatus>();
            foreach (var p in persons)
            {
                VMPersonStatus vmp = new VMPersonStatus();

                var inprogress_tasks = (from t in db.Tasks
                                        where p.PersonID == t.PersonID && t.Status == 0
                                        select t).ToList();


                var completed_tasks = (from t in db.Tasks
                                       where p.PersonID == t.PersonID && t.Status == 1
                                       select t).ToList();

                var compeleted_files = (from a in db.Tasks
                                        join b in db.AllocatedFiles
                                        on a.TaskID equals b.TaskID
                                        where a.PersonID == p.PersonID &&
                                        b.IsSubmited == true//Task_Status.Completed.ToString()
                                        select b).Count();

                var last_activity_count = (from a in db.PersonActivityLogs
                                     where a.ActivityStatus == 0 && a.PersonID == p.PersonID
                                     select a.LoginTime).Count();

                DateTime last_activity = DateTime.MinValue;
                if (last_activity_count > 0)
                {
                    last_activity = (from a in db.PersonActivityLogs
                                         where a.ActivityStatus == 0 && a.PersonID == p.PersonID
                                         select a.LoginTime).Max();
                }

                var today_compeleted_files = (from a in db.Tasks
                                              join b in db.AllocatedFiles
                                              on a.TaskID equals b.TaskID
                                              where a.PersonID == p.PersonID &&
                                              b.IsSubmited == true select b).ToList()
                                              .Where(
                                                        b=>b.SubmitedTime < DateTime.Now.AddDays(1).Date && b.SubmitedTime.Value.Date >= DateTime.Now.AddDays(-1).Date).Count();


                vmp.PersonName = p.PersonName;
                vmp.HaveProgressTask = inprogress_tasks.Count() > 0;
                vmp.TaskCompleted = completed_tasks.Count;
                vmp.LastLoginTime = last_activity;
                vmp.TodayFileCompleted = today_compeleted_files;
                vmp.FileCompeleted = compeleted_files;

                vMPersonStatuses.Add(vmp);

            }

            return View(vMPersonStatuses);
        }

        [HttpPost]
        public JsonResult FilePerGroup()
        {
            int ownerId = CurrentUser.PersonID;

            //var countPerGroup = from a in db.SpeechFiles.Where(p=>p.CreatorId == ownerId)
            //                    group a by a.Group.GroupName into g
            //                    select new
            //                    {
            //                        name = g.Key,
            //                        count = g.Count()
            //                    };

            var tablePerGroup = (from a in db.Groups join
                                 b in db.SpeechFiles.Where(p => p.CreatorId == ownerId && p.IsActive == true)
                                 on a.GroupID equals b.GroupID into joined
                                 from j in joined.DefaultIfEmpty()
                                    select new
                                    {
                                        g = a,
                                        s = j

                                    });

            var countPerGroup = from t in tablePerGroup
                                group t by t.g.GroupName into s
                                select new
                                {
                                    name = s.Key,
                                    count = s.Count(d => d.s.FileID != null)
                                };


            List<object> iData = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("GroupName", Type.GetType("System.String"));
            dt.Columns.Add("Count", Type.GetType("System.Int32"));

            foreach (var item in countPerGroup)
            {
                DataRow dr = dt.NewRow();
                dr["GroupName"] = item.name;
                dr["Count"] = item.count;
                dt.Rows.Add(dr);
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilePerGroupSubmitted()
        {
            int ownerId = CurrentUser.PersonID;

            var table_join_speech_allocated =
                (from speech in db.SpeechFiles.Where(p => p.CreatorId == ownerId && p.IsActive == true)
                 join
                      allocated in db.AllocatedFiles on speech.FileID equals allocated.FileID
                 where allocated.IsSubmited == true
                 select new {
                     fileID = speech.FileID,
                     groupID = speech.GroupID
                 });

            var tablePerGroup = (from a in db.Groups
                                 join
                                      b in table_join_speech_allocated
                                        on a.GroupID equals b.groupID into joined
                                 from j in joined.DefaultIfEmpty()
                                 select new
                                 {
                                     g = a,
                                     s = j

                                 });

            var countPerGroup = from t in tablePerGroup
                                group t by t.g.GroupName into s
                                select new
                                {
                                    name = s.Key,
                                    count = s.Count(d => d.s.fileID != null)
                                };

            List<object> iData = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("GroupName", Type.GetType("System.String"));
            dt.Columns.Add("Count", Type.GetType("System.Int32"));

            foreach (var item in countPerGroup)
            {
                DataRow dr = dt.NewRow();
                dr["GroupName"] = item.name;
                dr["Count"] = item.count;
                dt.Rows.Add(dr);
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TasksStatus()
        {
            int ownerId = CurrentUser.PersonID;
            var countPerGroup = from a in db.Tasks join b in db.Persons
                                on a.PersonID equals b.PersonID
                                where b.CreatorId == ownerId && b.IsDeleted == false
                                group a by a.Status into g
                                select new
                                {
                                    status = g.Key,
                                    count = g.Count()
                                };

            List<object> iData = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("TaskStatus", Type.GetType("System.String"));
            dt.Columns.Add("Count", Type.GetType("System.Int32"));

            foreach (var item in countPerGroup)
            {
                DataRow dr = dt.NewRow();
                if (item.status == (int)Task_Status.InProgress) dr["TaskStatus"] = "در حال انجام";
                if (item.status == (int)Task_Status.Completed) dr["TaskStatus"] = "تکمیل شده";
                if (item.status == (int)Task_Status.Withdraw) dr["TaskStatus"] = "پس گرفته شده";

                dr["Count"] = item.count;
                dt.Rows.Add(dr);
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult FilePerGroupHours()
        {
            int ownerId = CurrentUser.PersonID;

            var tablePerGroup = (from a in db.Groups
                                 join
                                    b in db.SpeechFiles.Where(p => p.CreatorId == ownerId && p.IsActive == true)
                                    on a.GroupID equals b.GroupID into joined
                                    from j in joined.DefaultIfEmpty()
                                 select new
                                 {
                                     g = a,
                                     s = j == null? 0 : j.FileDuration                                
                                 }).ToList();

            var countPerGroup = (from t in tablePerGroup
                                group t by t.g.GroupName into f
                                where f != null
                                select new
                                {
                                    name = f.Key,
                                    sum = f.Sum(p => p.s)
                                }).ToList(); 


            List<object> iData = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("GroupName", Type.GetType("System.String"));
            dt.Columns.Add("Sum", Type.GetType("System.Int32"));

            foreach (var item in countPerGroup)
            {
                DataRow dr = dt.NewRow();
                dr["GroupName"] = item.name;
                dr["Sum"] = item.sum/60;
                dt.Rows.Add(dr);
            }

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        ~HomeController()
        {
            db.Dispose();
        }

    }
}