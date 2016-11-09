using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public class UserService
    {
        public static User AddUser(string email)
        {
            User user = new User();
            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                User userE = dbVoteContest.Users.Where(x => x.emailUser == email).FirstOrDefault();
                if(userE == null)
                {
                    user = new User
                    {
                        emailUser = email
                    };
                    dbVoteContest.Users.Add(user);
                    dbVoteContest.SaveChanges();
                }
                else
                {
                    user = userE;
                }
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return user;
        }
    }
}