using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public static class VideoService
    {
        private static db_votecontestEntities dbVoteContest = new db_votecontestEntities();

        public static List<Video> GetVideosHome()
        {
            List<Video> listVideo = new List<Video>();

            List<Round> listRound = dbVoteContest.Rounds.ToList();
            foreach (var round in listRound)
            {
                List<Video> listVideoTemp = dbVoteContest.Videos.Where(x => x.idRound == round.idRound).OrderByDescending(x => x.VotingVideos.Count).Take(6).ToList();
                listVideo.AddRange(listVideoTemp);
            }
            return listVideo;
        }

        public static List<Video> GetVideosByRound_Most(int idRound, int skip, int count, out int total)
        {
            List<Video> listVideo = new List<Video>();

            var listVideoAll = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.VotingVideos.Count);
            total = listVideoAll.Count();

            listVideo = listVideoAll.Skip(skip).Take(count).ToList();

            return listVideo;
        }

        public static List<Video> GetVideosByRound_New(int idRound, int skip, int count, out int total)
        {
            List<Video> listVideo = new List<Video>();

            var listVideoAll = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderBy(x => x.createDate);
            total = listVideoAll.Count();

            listVideo = listVideoAll.Skip(skip).Take(count).ToList();

            return listVideo;
        }

        public static List<Video> GetAllVideosByNew(int idRound)
        {
            List<Video> listVideo = new List<Video>();

            var listVideoAll = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderBy(x => x.createDate).ToList();
            
            return listVideo;
        }

        public static List<Video> GetAllVideosByMost(int idRound)
        {
            List<Video> listVideo = new List<Video>();

            listVideo = dbVoteContest.Videos.Where(x => x.idRound == idRound).ToList();

            return listVideo;
        }

        public static Video GetVideoById(int idVideo)
        {
            Video video = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
            return video;
        }

        public static Video UpdateVideo(Video video)
        {
            Video newvideo = dbVoteContest.Videos.Where(x => x.idVideo == video.idVideo).FirstOrDefault();

            if(newvideo != null)
            {
                newvideo.authorVideo = video.authorVideo;
                newvideo.codeAuthor = video.codeAuthor;
                newvideo.describeVideo = video.describeVideo;
                newvideo.urlVideo = video.urlVideo;
                newvideo.titleVideo = video.titleVideo;
                newvideo.idRound = video.idRound;
                dbVoteContest.SaveChanges();
            }
            else
            {
                newvideo =  dbVoteContest.Videos.Add(video);
            }
            return newvideo;
        }
    }
}