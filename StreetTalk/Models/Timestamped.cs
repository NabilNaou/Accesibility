using System;

namespace StreetTalk.Models
{
    public class Timestamped
    {
        public DateTime? createdAt { get; }
        
        public DateTime? modifiedAt { get; set; }
        
        public Timestamped()
        {
            createdAt = DateTime.UtcNow;
            modifiedAt = DateTime.UtcNow;
        }
    }
}