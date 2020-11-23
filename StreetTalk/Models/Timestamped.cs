using System;

namespace StreetTalk.Models
{
    public class Timestamped
    {
        public virtual DateTime? createdAt { get; }
        
        public virtual DateTime? modifiedAt { get; set; }
        
        public Timestamped()
        {
            createdAt = DateTime.UtcNow;
            modifiedAt = DateTime.UtcNow;
        }
    }
}