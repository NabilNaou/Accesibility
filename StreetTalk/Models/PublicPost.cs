using System.Collections.Generic;

namespace StreetTalk.Models
{
    public class PublicPost : AnonymousPost
    {
        public bool closed { get; set; }
        
        public int reportCount { get; set; }
        
        public List<Comment> comments { get; } = new List<Comment>();
        
        public List<Like> likes { get; } = new List<Like>();
    }
}