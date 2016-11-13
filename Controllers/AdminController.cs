using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteClip.Models;

namespace VoteClip.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            List<VoteClip.Models.Round> listRound = VoteClip.Models.RoundService.GetAllRound();
            return View(listRound);
        }

        public ActionResult ListVideos(int idRound)
        {
            int sort = 1;
            List<Video> listVideo = new List<Video>();

            if (sort == 1)
            {
                listVideo = VideoService.GetAllVideosByMost(idRound);
            }
            else if (sort == 2)
            {
                listVideo = VideoService.GetAllVideosByNew(idRound);
            }

            ViewBag.IdRound = idRound;

            return View(listVideo);
        }

        public ActionResult EditRound(int idRound)
        {
            Round round = RoundService.GetRoundById(idRound);
            return View(round);
        }

        [HttpPost]
        public ActionResult UpdateRound(Round round)
        {
            Round newRound = RoundService.UpdateRound(round);
            ViewBag.IdRound = newRound.idRound;
            return View();
        }

        public ActionResult GetVideo(int? idVideo)
        {
            Video video = new Video();
            if (idVideo.HasValue)
            {
                video = VideoService.GetVideoById(((int)idVideo));
            }
            return View(video);
        }

        [HttpPost]
        public ActionResult GetVideo(Video video, int idRound)
        {
            bool isValide = true;
            if(string.IsNullOrEmpty(video.authorVideo))
            {
                ModelState.AddModelError("authorVideo", "Cần điền tên tác giả video");
            }
            if(string.IsNullOrEmpty(video.codeAuthor))
            {
                ModelState.AddModelError("codeAuthor", "Cần điền mã số sinh viên tác giả video");
            }
            if (string.IsNullOrEmpty(video.titleVideo))
            {
                ModelState.AddModelError("titleVideo", "Cần điền tên video");
            }
            if (string.IsNullOrEmpty(video.urlVideo))
            {
                ModelState.AddModelError("urlVideo", "Cần điền tên url video");
            }

            if(ModelState.IsValid)
            {
                video.idRound = idRound;
                Video newVideo = VideoService.UpdateVideo(video);
                return RedirectToAction("UpdateVideo", new { idRound = idRound });
            }
            else
            {
                return View(video);
            }
        }

        public ActionResult UpdateVideo(int idRound)
        {
            return View(idRound);
        }

        public ActionResult ListTags()
        {
            List<Tag> listTags = TagService.ListTags();
            return View(listTags);
        }

        [HttpPost]
        public ActionResult Upload(Tag tag, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    // Save file
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/uploads/"), fileName);
                    file.SaveAs(path);

                    // Update tag
                    tag.valueTag = "/uploads/" + fileName;
                    bool isSuccess = TagService.UpdateTag(tag);
                    if (isSuccess)
                    {
                        ViewBag.Message = "Upload successful";
                    }
                    else
                    {
                        ViewBag.Message = "Upload failed";
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("ListTags");
            }
        }

        [HttpPost]
        public ActionResult UpdateTag(string keyTag, string urlTag)
        {
            try
            {
                Tag tag = TagService.GetTag(keyTag);

                if (tag != null && urlTag != "")
                {
                    tag.valueTag = urlTag;
                    bool isSuccess = TagService.UpdateTag(tag);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("ListTags");
            }
        }
    }
}
