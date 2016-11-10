using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VoteClip.Models
{
    public class FileUpload
    {
        [DisplayName("Select File to Upload")]
        public HttpPostedFileBase File { get; set; }
    }
}