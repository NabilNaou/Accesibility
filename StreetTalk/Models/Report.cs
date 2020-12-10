namespace StreetTalk.Models
{
    public class Report
    {
        public virtual string Reason { get; set; }
        
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
        
        public virtual int PostId { get; set; }
        public virtual PublicPost Post { get; set; }
    }
}