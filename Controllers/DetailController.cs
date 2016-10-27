using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteClip.Models;

namespace VoteClip.Controllers
{
    public class DetailController : Controller
    {
        //
        // GET: /Detail/

        public ActionResult Round(int idRound, int page, int item, int sort) // sort: 1 is new, 2 is most
        {
            int total;
            int skip = page*item;
            List<Video> listVideo = new List<Video>();

            if (sort == 1)
            {
                listVideo = VideoService.GetVideosByRound_New(idRound, skip, item, out total);
            } else if (sort == 2)
            {
                listVideo = VideoService.GetVideosByRound_Most(idRound, skip, item, out total);
            }
            return View(listVideo);
        }

        public ActionResult Video(int idVideo)
        {
            Video video = VideoService.GetVideoById(idVideo);
            return View(video);
        }
    }
}
