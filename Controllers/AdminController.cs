using System;
using System.Collections.Generic;
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

        public ActionResult GetVideo(int idVideo)
        {
            Video video = new Video();
            if (idVideo != 0)
            {
                video = VideoService.GetVideoById(idVideo);
            }
            return View(video);
        }

        [HttpPost]
        public ActionResult UpdateVideo(Video video, int idRound)
        {
            video.idRound = idRound;
            Video newVideo = VideoService.UpdateVideo(video);
            ViewBag.IdRound = idRound;
            return View();
        }
    }
}
