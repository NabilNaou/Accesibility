using System;

namespace StreetTalk.Models
{
    public class Timestamped
    {
        public virtual DateTime? CreatedAt { get; set; }
        
        public virtual DateTime? ModifiedAt { get; set; }
        
        public Timestamped()
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}