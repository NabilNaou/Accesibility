using System;


namespace StreetTalk.Models
{
    public class Timestamped
    {
        public virtual DateTime? CreatedAt { get; set; }
        
        public virtual DateTime? ModifiedAt { get; set; }

        public DateTime? ToDate(DateTime? date)
        {
            DateTime temp = (DateTime) date;
            return temp.Date; 
        }

        protected Timestamped()
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}