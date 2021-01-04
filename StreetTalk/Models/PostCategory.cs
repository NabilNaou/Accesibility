using System.Collections.Generic;

namespace StreetTalk.Models
{
    public class PostCategory
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public virtual List<PublicPost> Posts { get; } = new List<PublicPost>();
    }
}