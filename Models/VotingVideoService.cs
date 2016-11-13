using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public class VotingVideoService
    {
        public static List<VotingVideo> GetListUserVote(int idVideo)
        {
            List<VotingVideo> listVotingVideo = new List<VotingVideo>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listVotingVideo = dbVoteContest.VotingVideos.Where(x => x.idVideo == idVideo).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVotingVideo;
        }
        public static List<VotingVideo> GetListUserVote(int idVideo, string codeAuthor)
        {
            List<VotingVideo> listVotingVideo = new List<VotingVideo>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listVotingVideo = dbVoteContest.VotingVideos.Where(x => x.idVideo == idVideo && x.codeAuthor == codeAuthor).ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listVotingVideo;
        }

        public static Video AddVote(int idVideo, string codeAuthor, int idUser)
        {
            Video videoVote = null;

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();

            try
            {
                Video video = VideoService.GetVideoById(idVideo);
                // Get round for check expire date
                Round round = RoundService.GetRoundById((int)video.idRound);
                DateTime now = DateTime.Now;

                // Check expire date
                if (round.startDate <= now && round.endDate >= now)
                {
                    // Get voting video of user
                    VotingVideo votingVideo = dbVoteContest.VotingVideos.Join(dbVoteContest.Videos, vote => vote.idVideo, v => v.idVideo, (vote, v) => new { Vote = vote, Video = v}).Where(x => x.Video.idRound == round.idRound && x.Vote.idUser == idUser).Select(x => x.Vote).FirstOrDefault();
                    // null : user do not vote video yet
                    if (votingVideo == null)
                    {
                        votingVideo = new VotingVideo();
                        votingVideo.idVideo = idVideo;
                        votingVideo.codeAuthor = codeAuthor;
                        votingVideo.idUser = idUser;

                        dbVoteContest.VotingVideos.Add(votingVideo);
                        dbVoteContest.SaveChanges();

                        videoVote = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
                        videoVote.codeAuthor = codeAuthor;
                    }
                    else // !null is check round
                    {
                        // get video voted
                        Video videoVoted = dbVoteContest.Videos.Where(x => x.idVideo == votingVideo.idVideo).FirstOrDefault();
                        //videoVote = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
                        //// Check round
                        //if (videoVoted.idRound != videoVote.idRound)
                        //{
                        //    votingVideo = new VotingVideo();
                        //    votingVideo.idVideo = idVideo;
                        //    votingVideo.codeAuthor = codeAuthor;
                        //    votingVideo.idUser = idUser;

                        //    dbVoteContest.VotingVideos.Add(votingVideo);
                        //    dbVoteContest.SaveChanges();

                        //    videoVote = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
                        //    videoVote.codeAuthor = codeAuthor;
                        //}
                        //else
                        //{
                        //    videoVote = videoVoted;
                        //    videoVote.codeAuthor = votingVideo.codeAuthor;
                        //}
                        videoVote = videoVoted;
                        videoVote.codeAuthor = votingVideo.codeAuthor;
                    }
                }
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }

            return videoVote;
        }
    }
}