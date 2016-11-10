using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public class TagService
    {
        public static List<Tag> ListTags()
        {
            List<Tag> listTags = new List<Tag>();
            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                listTags = dbVoteContest.Tags.ToList();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return listTags;
        }

        public static Tag GetTag(string keyTag)
        {
            Tag tags = new Tag();
            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                tags = dbVoteContest.Tags.Where(x => x.keyTag == keyTag).FirstOrDefault();
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return tags;
        }

        public static bool UpdateTag(Tag newtag)
        {
            bool isSucces = false;
            db_votecontestEntities dbVoteContest = new db_votecontestEntities();
            try
            {
                Tag tag = dbVoteContest.Tags.Where(x => x.idTag == newtag.idTag).FirstOrDefault();
                if (tag != null)
                {
                    tag.nameTag = newtag.nameTag;
                    tag.valueTag = newtag.valueTag;

                    dbVoteContest.SaveChanges();

                    isSucces = true;
                }
            }
            finally
            {
                if (dbVoteContest != null)
                {
                    ((IDisposable)dbVoteContest).Dispose();
                }
            }
            return isSucces;
        }
    }

}