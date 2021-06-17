using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DataLayer;
using SegamApp.Controllers;

namespace SegamApp.Areas.Daemon.Controllers
{
    [Authorize(Roles = "Daemon")]
    public class PeopleController : BaseController
    {
        private SegamDBContext db = new SegamDBContext();
        public int ownerID = 0;
        // GET: Daemon/People
        public ActionResult Index()
        {

            ownerID = CurrentUser.PersonID;
            var persons = db.Persons.Where(p => p.CreatorId == ownerID && p.IsDeleted == false);

            return View(persons.ToList());
        }

        // GET: Daemon/People/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Daemon/People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonName,PersonEmail,Password,IsDeleted,RoleID")] Person person)
        {
            Person p = db.Persons.Where(pt => pt.PersonEmail == person.PersonEmail && pt.IsDeleted == false).FirstOrDefault();
            if (p != null)
                ModelState.AddModelError("personEmail", "نام کاربری انتخاب شده تکراری می باشد");

            if (ModelState.IsValid)
            {
                ownerID = CurrentUser.PersonID;
                person.CreatorId = ownerID;
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // GET: Daemon/People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // POST: Daemon/People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,PersonName,PersonEmail,Password,IsDeleted,RoleID,CreatorId")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // GET: Daemon/People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Daemon/People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            person.IsDeleted = true;
            //db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
