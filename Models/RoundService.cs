using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public static class RoundService
    {
        public static List<Round> GetAllRound()
        {
            List<Round> listRound = new List<Round>();

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                DateTime? now = DateTime.Now;
                listRound = dbVoteContest.Rounds.ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listRound;
        }

        public static Round GetRoundById(int idRound)
        {
            Round round = null;

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                round = dbVoteContest.Rounds.Where(x => x.idRound == idRound).FirstOrDefault();
                List<Video> listVideo = dbVoteContest.Videos.Where(x => x.idRound == idRound).ToList();
                round.Videos = listVideo;
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return round;
        }

        public static Round UpdateRound(Round round)
        {
            Round newRound = null;

            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                newRound = dbVoteContest.Rounds.Where(x => x.idRound == round.idRound).FirstOrDefault();
                if (newRound != null)
                {
                    newRound.nameRound = round.nameRound;
                    newRound.ruleRound = round.ruleRound;
                    newRound.startDate = round.startDate;
                    newRound.endDate = round.endDate;
                    dbVoteContest.SaveChanges();
                }
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            
            return newRound;
        }
    }
}