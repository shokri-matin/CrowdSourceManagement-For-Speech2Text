using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using NAudio.Wave;
using SegamApp.Controllers;

namespace SegamApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileUploaderController : BaseController
    {
        SegamDBContext db = null;
        public FileUploaderController()
        {
            db = new SegamDBContext();
        }
        public ActionResult Index()
        {
            var groups = db.Groups;
            return View(groups);
        }

        [HttpPost]
        public ActionResult FileUpload(List<HttpPostedFileBase> files)
        {
            ViewBag.Success = "false";
            if (ModelState.IsValid)
            {
                var path = "";
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {
                            if (Path.GetExtension(file.FileName).ToLower() == ".mp3")
                            {
                                path = Path.Combine(Server.MapPath("~/Dataset"), file.FileName);
                                file.SaveAs(path);
                                ViewBag.Success = "true";
                            }
                        }
                    }
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult AjaxFileUpload(string id, int gid)
        {
            int ownerId = CurrentUser.PersonID;

            if (!Directory.Exists(Server.MapPath("~/Dataset/")))
                Directory.CreateDirectory(Server.MapPath("~/Dataset/"));

            if (!Directory.Exists(Server.MapPath("~/Dataset/" + ownerId + "/")))
                Directory.CreateDirectory(Server.MapPath("~/Dataset/" + ownerId + "/"));

            if (!Directory.Exists(Server.MapPath("~/Dataset/" + ownerId + "/" + id + "/")))
                Directory.CreateDirectory(Server.MapPath("~/Dataset/" + ownerId + "/" + id + "/"));


            if (Request.Files.Count > 0)
            {
                try
                {
                    string fname = "";
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        fname = file.FileName;

                        if (!(Path.GetExtension(fname).ToLower() == ".mp3" || Path.GetExtension(fname).ToLower() == ".wav"))
                            return Json(new { v1 = "error", v2 = "پسوند فایل باید .mp3 یا .wav  باشد", v3 = fname });


                        Guid FileName = Guid.NewGuid();

                        string physical_path = "~/Dataset/" + ownerId + "/" + id + "/";

                        string new_fname = Path.Combine(Server.MapPath(physical_path), FileName.ToString() + Path.GetExtension(fname).ToLower());
                        file.SaveAs(new_fname);

                        double duration = 0;

                        if (Path.GetExtension(fname).ToLower() == ".mp3")
                        {
                            //using (Mp3FileReader reader = new Mp3FileReader(file.InputStream))
                            //{
                            //    duration = reader.TotalTime.TotalSeconds;
                            //}

                            using (AudioFileReader reader = new AudioFileReader(new_fname))
                            {
                                duration = reader.TotalTime.TotalSeconds;
                            }
                        }
                        else
                        {
                            using (AudioFileReader reader = new AudioFileReader(new_fname))
                            {
                                duration = reader.TotalTime.TotalSeconds;
                            }
                            //using (WaveFileReader reader = new WaveFileReader(file.InputStream))
                            //{
                            //    duration = reader.TotalTime.TotalSeconds;
                            //}

                        }

                        db.SpeechFiles.Add(new SpeechFile()
                        {
                            FileID = FileName,
                            CreatorId = ownerId,
                            FileName = new_fname,
                            FileDuration = duration,
                            GroupID = gid,
                            FileType = Path.GetExtension(fname).ToLower(),
                            IsActive = false,
                            SuggestedText = "",
                            SequenceID = id,
                            PublishedDate = DateTime.Now
                        });
                    }

                    db.SaveChanges();

                    return Json(new { v1 = "ok", v2 = "", v3 = fname });
                }
                catch (Exception ex)
                {
                    return Json(new { v1 = "error", v2 = "Error occurred. Error details: " + ex.Message, v3 = "" });
                }
            }
            else
            {
                return Json(new { v1 = "error", v2 = "لطفا حداقل یک فایل انتخاب فرمایید", v3 = "" });
            }
        }

        ~FileUploaderController()
        {
            db.Dispose();
        }
    }
}