using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteClip.Models;

namespace VoteClip.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<Round> listRound = RoundService.GetAllRound();
            //List<Video> listVideo = VideoService.GetVideosHome();
            return View(listRound);
        }
    }
}
