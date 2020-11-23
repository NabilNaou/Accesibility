using System.Collections.Generic;

namespace StreetTalk.Models
{
    public class PublicPost : Post
    {
        public virtual bool closed { get; set; }
        
        public virtual int reportCount { get; set; }
        
        public virtual List<Comment> comments { get; } = new List<Comment>();
        
        public virtual List<Like> likes { get; } = new List<Like>();
    }
}