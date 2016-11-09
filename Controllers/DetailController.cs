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

        public ActionResult Round(int idRound, int? page, int? item, int? sort) // sort: 1 is new, 2 is most
        {
            int total;

            page = page.HasValue ? page : 0;
            item = item.HasValue ? item : 10;
            sort = sort.HasValue ? sort : 1;

            int skip = (int)page*(int)item;
            List<Video> listVideo = new List<Video>();

            if (sort == 1)
            {
                listVideo = VideoService.GetVideosByRound_New(idRound, skip, (int) item, out total);
            } else if (sort == 2)
            {
                listVideo = VideoService.GetVideosByRound_Most(idRound, skip, (int) item, out total);
            }

            ViewBag.IdRound = idRound;

            return View(listVideo);
        }

        public ActionResult Video(int idVideo)
        {
            Video video = VideoService.GetVideoById(idVideo);
            return View(video);
        }

        public ActionResult VoteVideo(int idVideo, string codeAuthor, string email)
        {
            User user = UserService.AddUser(email);
            Video videoVote = VotingVideoService.AddVote(idVideo, codeAuthor, user.idUser);
            bool isVoteSuccess = false;

            if(videoVote.idVideo == idVideo)
            {
                isVoteSuccess = true;
            }

            return base.Json(new { response = isVoteSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}
