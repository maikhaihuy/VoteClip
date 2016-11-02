using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public static class RoundService
    {
        private static db_votecontestEntities dbVoteContest = new db_votecontestEntities();

        public static List<Round> GetAllRound()
        {
            List<Round> listRound = new List<Round>();
            DateTime? now = DateTime.Now;
            listRound = dbVoteContest.Rounds.ToList();
            return listRound;
        }

        public static Round GetRoundById(int idRound)
        {
            Round round = dbVoteContest.Rounds.Where(x => x.idRound == idRound).FirstOrDefault();
            
            return round;
        }

        public static Round UpdateRound(Round round)
        {
            Round newRound = dbVoteContest.Rounds.Where(x => x.idRound == round.idRound).FirstOrDefault();
            if(newRound != null)
            {
                newRound.nameRound = round.nameRound;
                newRound.ruleRound = round.ruleRound;
                newRound.startDate = round.startDate;
                newRound.endDate = round.endDate;
                dbVoteContest.SaveChanges();
            }
            return newRound;
        }
    }
}