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

            //if (sort == 1)
            //{
            //    listVideo = VideoService.GetVideosByRound_New(idRound, skip, (int) item, out total);
            //} else if (sort == 2)
            //{
            //    listVideo = VideoService.GetVideosByRound_Most(idRound, skip, (int) item, out total);
            //}

            listVideo = VideoService.GetAllVideosByMost(idRound);

            ViewBag.IdRound = idRound;

            return View(listVideo);
        }

        public ActionResult Video(int idVideo)
        {
            Video video = new Video();
            if (idVideo != 0)
            {
                video = VideoService.GetVideoById(idVideo);
            }
            return View(video);
        }

        public JsonResult VoteVideo(int idVideo, string codeAuthor, string email)
        {
            ModelVideoVote modelVideo = new ModelVideoVote();

            if (!string.IsNullOrEmpty(email) && !email.Equals("undefined"))
            {
                User user = UserService.AddUser(email);
                Video videoVote = VotingVideoService.AddVote(idVideo, codeAuthor, user.idUser);

                if(videoVote == null)
                {
                    modelVideo.message = "Chưa đến hạn hoặc hết hạn bình chọn.";
                }
                else
                if (videoVote.idVideo == idVideo && videoVote.codeAuthor.Equals(codeAuthor))
                {
                    modelVideo.message = "Bạn đã bình chọn thành công cho video " + videoVote.titleVideo + " của thí sinh " + videoVote.codeAuthor;
                }
                else
                {
                    modelVideo.message = "Bạn đã bình chọn " + videoVote.titleVideo + " của thí sinh " + videoVote.codeAuthor + " bạn không thể bình chọn tiếp.";
                }
            }
            else
            {
                modelVideo.message = "Lỗi kết nối!!!";
            }
            return base.Json(modelVideo, JsonRequestBehavior.AllowGet);
        }

        class ModelVideoVote
        {
            public string titleVideo { get; set; }
            public string codeAuthor { get; set; }
            public string message { get; set; }
        }
    }
}
