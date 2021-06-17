using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SegamApp.Controllers;
using DataLayer;
using DataLayer.ViewModel;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileConfirmationController : BaseController
    {
        SegamDBContext db = null;
        public FileConfirmationController()
        {
            db = new SegamDBContext();
        }
        // GET: Admin/FileConfirmation
        public ActionResult Index()
        {
            int ownerId = CurrentUser.PersonID;

            List<VMFileConfirmation> files = (from f in
                                          db.SpeechFiles.Where(p => p.IsActive == false && p.CreatorId == ownerId)
                                              join e in db.Groups on f.GroupID equals e.GroupID
                                              orderby f.PublishedDate descending
                                              select new VMFileConfirmation()
                                              {
                                                  FileID = f.FileID,
                                                  SequenceID = f.SequenceID,
                                                  GroupID = f.GroupID,
                                                  GroupName = e.GroupName,
                                                  CreatorId = f.CreatorId,
                                                  FileType = f.FileType,
                                                  FileDuration = f.FileDuration,
                                                  PublishedDate = f.PublishedDate
                                              }

                                     ).ToList();

            return View(files);
        }

        public ActionResult Confirm(string sequenceId)
        {
            List<SpeechFile> sfiles = (from a in db.SpeechFiles.Where(p => p.SequenceID == sequenceId) select a).ToList();
            foreach (var item in sfiles)
            {
                item.IsActive = true;
            }

            db.SaveChanges();

            int ownerId = CurrentUser.PersonID;
            List<VMFileConfirmation> files = (from f in
                              db.SpeechFiles.Where(p => p.IsActive == false && p.CreatorId == ownerId)
                                              join e in db.Groups on f.GroupID equals e.GroupID
                                              orderby f.PublishedDate ascending
                                              select new VMFileConfirmation()
                                              {
                                                  FileID = f.FileID,
                                                  SequenceID = f.SequenceID,
                                                  GroupID = f.GroupID,
                                                  GroupName = e.GroupName,
                                                  CreatorId = f.CreatorId,
                                                  FileType = f.FileType,
                                                  FileDuration = f.FileDuration,
                                                  PublishedDate = f.PublishedDate
                                              }

                         ).ToList();

            return View("Index", files);
        }
        public ActionResult Delete(string sequenceId)
        {
            List<SpeechFile> sfiles = (from a in db.SpeechFiles.Where(p => p.SequenceID == sequenceId) select a).ToList();
            foreach (var item in sfiles)
            {
                db.SpeechFiles.Remove(item);
            }

            db.SaveChanges();

            int ownerId = CurrentUser.PersonID;
            List<VMFileConfirmation> files = (from f in
                              db.SpeechFiles.Where(p => p.IsActive == false && p.CreatorId == ownerId)
                                              join e in db.Groups on f.GroupID equals e.GroupID
                                              orderby f.PublishedDate ascending
                                              select new VMFileConfirmation()
                                              {
                                                  FileID = f.FileID,
                                                  SequenceID = f.SequenceID,
                                                  GroupID = f.GroupID,
                                                  GroupName = e.GroupName,
                                                  CreatorId = f.CreatorId,
                                                  FileType = f.FileType,
                                                  FileDuration = f.FileDuration,
                                                  PublishedDate = f.PublishedDate
                                              }

                         ).ToList();

            return View("Index", files);
        }
    }
}