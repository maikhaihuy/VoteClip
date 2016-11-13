using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace VoteClip.Models
{
    public static class VideoService
    {
        public static List<Video> GetVideosHome()
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                List<Round> listRound = dbVoteContest.Rounds.ToList();
                foreach (var round in listRound)
                {
                    List<Video> listVideoTemp =
                        dbVoteContest.Videos.Where(x => x.idRound == round.idRound)
                            .OrderByDescending(x => x.VotingVideos.Count)
                            .Take(6)
                            .ToList();
                    listVideo.AddRange(listVideoTemp);
                }
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVideo;
        }

        public static List<Video> GetVideosByRound_Most(int idRound, int skip, int count, out int total)
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                var listVideoAll =
                    dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.VotingVideos.Count());
                total = listVideoAll.Count();

                listVideo = listVideoAll.Skip(skip).Take(count).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVideo;
        }

        public static List<Video> GetVideosByRound_New(int idRound, int skip, int count, out int total)
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                var listVideoAll = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.createDate);
                total = listVideoAll.Count();

                listVideo = listVideoAll.Skip(skip).Take(count).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }

            return listVideo;
        }

        public static List<Video> GetAllVideosByNew(int idRound)
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listVideo = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.createDate).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVideo;
        }

        public static List<Video> GetAllVideosByMost(int idRound)
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listVideo = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.VotingVideos.Count()).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVideo;
        }

        public static List<Video> GetHomeVideosByRound(int idRound)
        {
            List<Video> listVideo = new List<Video>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listVideo = dbVoteContest.Videos.Where(x => x.idRound == idRound).OrderByDescending(x => x.VotingVideos.Count()).Take(6).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVideo;
        }

        public static Video GetVideoById(int idVideo)
        {
            Video video = new Video();
            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                video = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return video;
        }

        public static Video UpdateVideo(Video video)
        {
            Video newvideo = new Video();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                newvideo = dbVoteContest.Videos.Where(x => x.idVideo == video.idVideo).FirstOrDefault();
                if (newvideo != null)
                {
                    newvideo.authorVideo = video.authorVideo;
                    newvideo.codeAuthor = video.codeAuthor;
                    newvideo.describeVideo = video.describeVideo;
                    newvideo.urlVideo = video.urlVideo;
                    newvideo.titleVideo = video.titleVideo;
                    newvideo.idRound = video.idRound;
                }
                else
                {
                    video.createDate = DateTime.Now;
                    newvideo = dbVoteContest.Videos.Add(video);
                }

                dbVoteContest.SaveChanges();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            
            return newvideo;
        }
    }
}