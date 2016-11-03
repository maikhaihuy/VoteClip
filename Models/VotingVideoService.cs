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
    }
}