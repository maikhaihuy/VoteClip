//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VoteClip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VotingVideo
    {
        public int idVotingVideo { get; set; }
        public Nullable<int> idVideo { get; set; }
        public Nullable<int> idUser { get; set; }
    
        public virtual User User { get; set; }
        public virtual Video Video { get; set; }
    }
}