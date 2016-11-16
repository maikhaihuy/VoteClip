using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoteClip.Models;

namespace VoteClip.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Rule()
        {
            Tag tag = TagService.GetTag("RULE_PDF");
            return View(tag);
        }

    }
}
