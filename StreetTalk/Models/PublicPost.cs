using System.Collections.Generic;

namespace StreetTalk.Models
{
    public class PublicPost : Post
    {
        public virtual bool Closed { get; set; }
        
        public virtual int ReportCount { get; set; }

        public virtual List<Comment> Comments { get; } = new List<Comment>();
        
        public virtual List<Like> Likes { get; } = new List<Like>();
    }
}