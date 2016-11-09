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
                // Add vote + check vote
                VotingVideo votingVideo = dbVoteContest.VotingVideos.Where(x => x.idUser == idUser && x.codeAuthor == codeAuthor).FirstOrDefault();
                if(votingVideo == null)
                {
                    votingVideo = new VotingVideo();
                    votingVideo.idVideo = idVideo;
                    votingVideo.codeAuthor = codeAuthor;
                    votingVideo.idUser = idUser;

                    dbVoteContest.VotingVideos.Add(votingVideo);
                    dbVoteContest.SaveChanges();

                    videoVote = dbVoteContest.Videos.Where(x => x.idVideo == idVideo).FirstOrDefault();
                }
                else
                {
                    videoVote = dbVoteContest.Videos.Where(x => x.idVideo == votingVideo.idVideo).FirstOrDefault();
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