using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using SegamApp.Controllers;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : BaseController
    {
        private SegamDBContext db = null;
        public int ownerID = 0;
        public UserManagementController()
        {
            db = new SegamDBContext();
        }

        // GET: Admin/UserManagement
        public ActionResult Index()
        {

            ownerID = CurrentUser.PersonID;

            var persons = db.Persons.Where(p => p.CreatorId == ownerID && p.IsDeleted == false).Include(p => p.Role);
            return View(persons.ToList());
        }

        // GET: Admin/UserManagement/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/UserManagement/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles.Where(p => p.RoleID == (int)RolesStatus.User), "RoleID", "RoleName");
            return View();
        }

        // POST: Admin/UserManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,PersonName,PersonEmail,Password,IsDeleted,RoleID")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.CreatorId = CurrentUser.PersonID;

                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles.Where(p => p.RoleID == (int)RolesStatus.User), "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // GET: Admin/UserManagement/Edit/5
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
            ViewBag.RoleID = new SelectList(db.Roles.Where(p => p.RoleID == (int)RolesStatus.User), "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // POST: Admin/UserManagement/Edit/5
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
            ViewBag.RoleID = new SelectList(db.Roles.Where(p => p.RoleID == (int)RolesStatus.User), "RoleID", "RoleName", person.RoleID);
            return View(person);
        }

        // GET: Admin/UserManagement/Delete/5
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

        // POST: Admin/UserManagement/Delete/5
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
